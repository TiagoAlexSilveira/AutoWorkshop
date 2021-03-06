﻿using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    [Authorize(Roles ="Admin , Mechanic")]
    public class MechanicsController : Controller
    {
        private readonly IRepairRepository _repairRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMechanicRepository _mechanicRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly ISpecialtyRepository _specialtyRepository;

        public MechanicsController(IRepairRepository repairRepository,
                                   IAppointmentRepository appointmentRepository,
                                   IMechanicRepository mechanicRepository,
                                   IConverterHelper converterHelper,
                                   IUserHelper userHelper, IImageHelper imageHelper,
                                   ISpecialtyRepository specialtyRepository)
        {
            _repairRepository = repairRepository;
            _appointmentRepository = appointmentRepository;
            _mechanicRepository = mechanicRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _specialtyRepository = specialtyRepository;
        }

        // GET: Mechanics
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ssIndex()
        {

            return View(_mechanicRepository.GetAll().Include(u => u.Specialty));
        }


        public IActionResult WorkAppointments()
        {
            var appointments = _appointmentRepository.GetAll().Include(a => a.AppointmentType)
                                                              .Include(c => c.Client)
                                                              .Include(v => v.Vehicle).ThenInclude(v => v.Brand)
                                                              .Include(m => m.Mechanic)
                                                              .Where(m => m.Mechanic.User.UserName == User.Identity.Name && m.IsConfirmed == true && m.StartTime >= DateTime.Now);

            var amodel = new MecAppointViewModel
            {
                Appointments = appointments
            };

            return View(amodel);
        }



        public IActionResult MyRepairs()
        {
            var repair = _repairRepository.GetAll().Include(m => m.Appointment).ThenInclude(c => c.Client)
                                                   .Include(m => m.Appointment).ThenInclude(c => c.Mechanic)
                                                   .Include(m => m.Appointment).ThenInclude(c => c.Vehicle)
                                                   .Include(m => m.Appointment).ThenInclude(c => c.AppointmentType)
                                                   .Where(p => p.Appointment.Mechanic.User.Email == User.Identity.Name);

            var rmodel = new MecRepairsViewModel
            {
                Repairs = repair
            };

            return View(rmodel);
        }


        public async Task<IActionResult> MechanicDetails(int id)
        {
            var mechanic = await _mechanicRepository.GetMechanicWithSpecialtyById(id);
           
            var model = _converterHelper.ToPersonEditViewModel(mechanic);

            return PartialView("_MechanicDetailsPartial", model);
        }


        //// GET: Mecanics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanic = await _mechanicRepository.GetMechanicWithSpecialtyById(id.Value);
            if (mechanic == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPersonEditViewModel(mechanic);         
            model.Specialties = _specialtyRepository.GetComboSpecialty();
            model.SpecialtyId = mechanic.SpecialtyId;

            return View(model);
        }



        //// POST: Mecanics/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "People");
                }


                try
                {
                    var mechanic = _converterHelper.ToMechanicEdit(model, path);

                    await _mechanicRepository.UpdateAsync(mechanic);

                    ViewBag.UserMessage = "User Sucessfully Updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _mechanicRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(model);
            }
            return View(model);
        }



        //// GET: Mecanics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanic = await _mechanicRepository.GetByIdAsync(id.Value);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }


        //// POST: Mecanics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mechanic = await _mechanicRepository.GetByIdAsync(id);
            var user = await _userHelper.GetUserByIdAsync(mechanic.UserId);

            await _mechanicRepository.DeleteAsync(mechanic);
            await _userHelper.DeleteUserAsync(user);

            return RedirectToAction("ssIndex", "Mechanics");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Models;
using AutoWorkshop.Web.Helpers;

namespace AutoWorkshop.Web.Controllers
{
    public class RepairsController : Controller
    {
        
        private readonly IRepairRepository _repairRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public RepairsController(IRepairRepository repairRepository,
                                 IClientRepository clientRepository,
                                 IAppointmentRepository appointmentRepository,
                                 IConverterHelper converterHelper,
                                 IUserHelper userHelper)
        {
            
            _repairRepository = repairRepository;
            _clientRepository = clientRepository;
            _appointmentRepository = appointmentRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }


        // GET: Repairs
        public IActionResult Index()
        {
            var repair = _repairRepository.GetAll().Include(r => r.Appointment).ThenInclude(v => v.Vehicle).ThenInclude(b => b.Brand)
                                                   .Include(r => r.Appointment).ThenInclude(c => c.Client)
                                                   .Include(r => r.Appointment).ThenInclude(c => c.Mechanic)
                                                   .Include(r => r.Appointment).ThenInclude(c => c.AppointmentType).ToList();

            foreach (var rep in repair) //formatar a data
            {
                rep.FormatedDate = rep.CompletedAt.ToString("dd/MM/yyyy hh:mm");
            }
                                                   
            return View(repair);
        }



        //// GET: Repairs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var repair = await _repairRepository.GetByIdAsync(id.Value);
        //    if (repair == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(repair);
        //}



        // GET: Repairs/Create
        public IActionResult Create()
        {
            if (User.IsInRole("Mechanic"))
            {
                var model = new RepairViewModel
                {
                    Appointments = _appointmentRepository.GetComboUserAppointment(User.Identity.Name)
                };

                return View(model);
            }
            else
            {
                var model = new RepairViewModel
                {
                    Appointments = _appointmentRepository.GetComboAppointment()
                };

                return View(model);
            }
           
        }



        //POST: Repairs/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Repair repair)
        {
            if (ModelState.IsValid)
            {
                var client = _clientRepository.GetClientByUserEmail(User.Identity.Name);


                await _repairRepository.CreateAsync(repair);

                if (User.IsInRole("Mechanic"))
                {
                    return RedirectToAction("MyRepairs", "Mechanics");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(repair);
        }

        //// GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _repairRepository.GetByIdWithAppointment(id.Value);
            if (repair == null)
            {
                return NotFound();
            }

            var vmodel = _converterHelper.ToRepairViewModel(repair);

            if (User.IsInRole("Mechanic"))
            {
                vmodel.Appointments = _appointmentRepository.GetComboUserAppointment(User.Identity.Name);
                vmodel.AppointmentId = repair.Appointment.Id;
            }
            else
            {
                vmodel.Appointments = _appointmentRepository.GetComboAppointment();
                vmodel.AppointmentId = repair.Appointment.Id;
            }

            return View(vmodel);
        }


        //// POST: Repairs/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RepairViewModel vmodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var repair = _converterHelper.ToRepair(vmodel);
                    repair.AppointmentId = vmodel.AppointmentId;

                    await _repairRepository.UpdateAsync(repair);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repairRepository.ExistAsync(vmodel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (User.IsInRole("Mechanic"))
                {
                    return RedirectToAction("MyRepairs", "Mechanics");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(vmodel);
        }


        //// GET: Repairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _repairRepository.GetByIdAsync(id.Value);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }


        //// POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repair = await _repairRepository.GetByIdAsync(id);
            await _repairRepository.DeleteAsync(repair);
            return RedirectToAction(nameof(Index));
        }

    }
}

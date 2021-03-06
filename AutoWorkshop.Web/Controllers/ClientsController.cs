﻿using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    [Authorize(Roles = "Admin, Client")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;

        public ClientsController(IClientRepository clientRepository,
                                 IVehicleRepository vehicleRepository,
                                 IAppointmentRepository appointmentRepository,
                                 IConverterHelper converterHelper,
                                 IImageHelper imageHelper, IUserHelper userHelper)
        {
            _clientRepository = clientRepository;
            _vehicleRepository = vehicleRepository;
            _appointmentRepository = appointmentRepository;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
        }

        // GET: Clients
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult MyVehicles()
        {

            var vehicles = _vehicleRepository.GetAll().Include(b => b.Brand)
                                                       .Where(p => p.Client.User.UserName == User.Identity.Name);

            var vmodel = new ClientMyVehiclesViewModel
            {
                Vehicles = vehicles.ToList()
            };

            return View(vmodel);
        }



        public async Task<IActionResult> MyAppointments()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            var client = _clientRepository.GetClientByUserId(user.Id);

            var uncappointment = _appointmentRepository.GetAll().Include(m => m.AppointmentType)
                                                              .Include(v => v.Vehicle).ThenInclude(b => b.Brand)
                                                              .Where(p => p.ClientId == client.Id && p.IsConfirmed != true);

            var appointments = _appointmentRepository.GetAll().Include(m => m.AppointmentType)
                                                                .Include(p => p.Vehicle).ThenInclude(g => g.Brand)
                                                                .Include(m => m.Mechanic)
                                                                .Where(p => p.ClientId == client.Id && p.IsConfirmed == true);

            var model = new ClientMyAppointmentsViewModel
            {
                Appointments = appointments.ToList(),
                UnconfirmedAppointments = uncappointment.ToList()
            };

            return View(model);
        }

        public IActionResult ssIndex()
        {
            return View(_clientRepository.GetAll().Include(u => u.User));
        }


        public async Task<IActionResult> ClientDetails(int Id)
        {
            var client = await _clientRepository.GetByIdAsync(Id);

            var model = _converterHelper.ToPersonEditViewModel(client);

            return PartialView("_ClientsPartial", model);
        }



        //GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPersonEditViewModel(client);

            return View(model);
        }

        //POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    var client = _converterHelper.ToClientEdit(model, path);

                    await _clientRepository.UpdateAsync(client);

                    ViewBag.UserMessage = "User Sucessfully Updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _clientRepository.ExistAsync(model.Id))
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


        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            var user = await _userHelper.GetUserByIdAsync(client.UserId);
            var vehicle = _vehicleRepository.GetAll().Where(u => u.ClientId == client.Id);
            var appoint = _appointmentRepository.GetAll().Where(a => a.ClientId == client.Id && a.IsConfirmed == false);


            await _clientRepository.DeleteAsync(client);
            await _userHelper.DeleteUserAsync(user);

            if (vehicle != null)
            {
                foreach (var vec in vehicle)
                {
                    await _vehicleRepository.DeleteAsync(vec);
                }
            }

            if (appoint != null)
            {
                foreach (var app in appoint)
                {
                    await _appointmentRepository.DeleteAsync(app);
                }
            }

            return RedirectToAction("ssIndex", "Clients");
        }



    }
}

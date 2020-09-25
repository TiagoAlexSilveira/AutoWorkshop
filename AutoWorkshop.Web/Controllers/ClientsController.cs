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
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConverterHelper _converterHelper;

        public ClientsController(IClientRepository clientRepository,
                                 IVehicleRepository vehicleRepository,
                                 IAppointmentRepository appointmentRepository,
                                 IConverterHelper converterHelper)
        { 
            _clientRepository = clientRepository;
            _vehicleRepository = vehicleRepository;
            _appointmentRepository = appointmentRepository;
            _converterHelper = converterHelper;
        }

        // GET: Clients
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult MyVehicles()
        {

            var vehicles = _vehicleRepository.GetAll().Include(b => b.Brand)
                                                       .Where(p => p.User.UserName == User.Identity.Name);

            var vmodel = new ClientMyVehiclesViewModel
            {
                Vehicles = vehicles.ToList()
            };

            return View(vmodel);
        }



        public IActionResult MyAppointments()
        {
            var appointments = _appointmentRepository.GetAll().Include(v => v.Vehicle)
                                                             .Include(c => c.Client)
                                                             .Where(p => p.Client.User.UserName == User.Identity.Name);

            var vmodel = new ClientMyAppointmentsViewModel
            {
                Appointments = appointments.ToList()
            };

            return View(vmodel);
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
                try
                {
                    var client = _converterHelper.ToClientEdit(model);

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
            await _clientRepository.DeleteAsync(client);
            return View("Index");
        }


        //TODO: meter um IsActive, no apagar meter IsActive a false, no GET ir só buscar IsActive = true
    }
}

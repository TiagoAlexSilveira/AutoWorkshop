using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using AutoWorkshop.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AutoWorkshop.Web.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentTypeRepository _appointmentTypeRepository;
        private readonly IMechanicRepository _mechanicRepository;
        private readonly IMailHelper _mailHelper;

        public AppointmentsController(IAppointmentRepository appointmentRepository,
                                      IVehicleRepository vehicleRepository,
                                      IConverterHelper converterHelper,
                                      IUserHelper userHelper,
                                      IClientRepository clientRepository,
                                      IAppointmentTypeRepository appointmentTypeRepository,
                                      IMechanicRepository mechanicRepository,
                                      IMailHelper mailHelper)
        {
            _appointmentRepository = appointmentRepository;
            _vehicleRepository = vehicleRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _clientRepository = clientRepository;
            _appointmentTypeRepository = appointmentTypeRepository;
            _mechanicRepository = mechanicRepository;
            _mailHelper = mailHelper;
        }



        // GET: Appointments/Create     //Create com o syncfusion
        public IActionResult Create()
        {
            var unconfirmedAppointments = _appointmentRepository.GetAll().Include(v => v.Vehicle)
                                                                        .Include(c => c.Client)
                                                                        .Include(m => m.Mechanic)
                                                                        .ThenInclude(c => c.Specialty)
                                                                        .Where(p => p.IsConfirmed != true);



            var model = new AppointmentViewModel
            {
                Vehicles = _vehicleRepository.GetAll().ToList(),
                AppointmentTypes = _appointmentTypeRepository.GetAll().ToList(),
                Appointments = _appointmentRepository.GetAll().Where(m => m.IsConfirmed == true).ToList(),
                Mechanics = _mechanicRepository.GetAll().ToList(),
                Clients =  _clientRepository.GetAll().ToList(),
                UnconfirmedAppointments = unconfirmedAppointments,
                MechanicsCombo = _mechanicRepository.GetComboMecanics()
            };

            return View(model);
        }


        public IActionResult CreateClient()
        {
            var confirmedAppointments = _appointmentRepository.GetAll().Include(v => v.Vehicle).ThenInclude(b => b.Brand)
                                                                       .Include(a => a.AppointmentType)
                                                                       .Where(p => p.IsConfirmed == true);


            var model = new AppointmentViewModel
            {
                Vehicles = _vehicleRepository.GetAll().Where(m => m.Client.User.UserName == User.Identity.Name).ToList(),
                Appointments = _appointmentRepository.GetAll().Where(m => m.IsConfirmed == true).ToList(),
                AppointmentTypes = _appointmentTypeRepository.GetAll().ToList(),
                UnconfirmedAppointments = confirmedAppointments,
                MechanicsCombo = _mechanicRepository.GetComboMecanics()
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> CreateClient(Appointment appointment)             
        {

            var client = _clientRepository.GetClientByUserEmail(User.Identity.Name);

            appointment.ClientId = client.Id;
            appointment.Id = 0;
            appointment.IsConfirmed = false;
            await _appointmentRepository.CreateAsync(appointment);

            return RedirectToAction("CreateClient", "Appointments");
        }


        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            appointment.Id = 0;
            appointment.IsConfirmed = true;
            await _appointmentRepository.CreateAsync(appointment);

            return RedirectToAction("Create", "Appointments");
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Appointment appointment)
        {          
            await _appointmentRepository.UpdateAsync(appointment);

            return RedirectToAction("Create", "Appointments");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            await _appointmentRepository.DeleteAsync(appointment);

            return RedirectToAction("Create", "Appointments");
        }


        public async Task<IActionResult> GetClient(int Id)
        {
            var client = await _clientRepository.GetByIdAsync(Id);

            return Json(client.PhoneNumber);
        }

        public async Task<IActionResult> GetClientVehicles(int Id)
        {
            var client = await _clientRepository.GetByIdAsync(Id);
            var vehicles = _vehicleRepository.GetAll().Where(c => c.ClientId == client.Id);

            return Json(vehicles);
        }

        public async Task<IActionResult> GetVehicle(int Id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(Id);

            return Json(vehicle.LicensePlate);
        }


        public async Task<IActionResult> GetAppointmentType(int Id)
        {
            var appointmentType = await _appointmentTypeRepository.GetByIdAsync(Id);

            return Json(appointmentType.Type);
        }


        public async Task<IActionResult> GetMechanic(int Id)
        {
            var mechanic = await _mechanicRepository.GetByIdAsync(Id);

            return Json(mechanic.FullName);
        }


        public async Task<IActionResult> Update(AppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.MechanicId == 0)
                {
                    return RedirectToAction("Create");
                }

                var appointment = await _appointmentRepository.GetByIdAsync(model.Id);

                appointment.MechanicId = model.MechanicId;
                appointment.IsConfirmed = true;
                await _appointmentRepository.UpdateAsync(appointment);

                return RedirectToAction("Create");
            }

            return RedirectToAction("Create");
        }


        public async Task<IActionResult> Cancel(int? id, int clientId, DateTime time)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.GetByIdAsync(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }

            await _appointmentRepository.DeleteAsync(appointment);

            var client = await _clientRepository.GetByIdAsync(clientId);
            var user = _clientRepository.GetUserByClientId(clientId);

            try
            {
                _mailHelper.SendMail(user.UserName, "Appointment Canceled", $"<h2>Mr(s) {client.FullName}</h2>" +
                $"<br><br><p>There have been some complications regarding your scheduled appointment for {time.ToShortDateString()} and therefore we will have to cancel it</p>" +
                $" <br><br>If you wish to reschedule an appointment, address our website at a later time <br><br>Apologies<br>AutoWorkShop. " );
            }
            catch (Exception e)
            {

                throw e;
            }

            return RedirectToAction("Create");
        }

    }
}

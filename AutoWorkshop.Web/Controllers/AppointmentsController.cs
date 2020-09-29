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

namespace AutoWorkshop.Web.Controllers
{
    public class AppointmentsController : Controller
    {

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentTypeRepository _appointmentTypeRepository;
        private readonly IMechanicRepository _mechanicRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository,
                                      IVehicleRepository vehicleRepository,
                                      IConverterHelper converterHelper,
                                      IUserHelper userHelper,
                                      IClientRepository clientRepository,
                                      IAppointmentTypeRepository appointmentTypeRepository,
                                      IMechanicRepository mechanicRepository)
        {
            _appointmentRepository = appointmentRepository;
            _vehicleRepository = vehicleRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _clientRepository = clientRepository;
            _appointmentTypeRepository = appointmentTypeRepository;
            _mechanicRepository = mechanicRepository;
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
                var appointment = await _appointmentRepository.GetByIdAsync(model.Id);

                appointment.MechanicId = model.MechanicId;
                appointment.IsConfirmed = true;
                await _appointmentRepository.UpdateAsync(appointment);

                return RedirectToAction("Create");
            }

            return RedirectToAction("Create");
        }

        //public async Task<JsonResult> Update(AppointmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var appointment = await _appointmentRepository.GetByIdAsync(model.Id);

        //        appointment.MechanicId = model.MechanicId;
        //        appointment.IsConfirmed = true;
        //        await _appointmentRepository.UpdateAsync(appointment);

        //        return Json("nice");
        //    }

        //    return Json(false);
        //}

        //// GET: Appointments/Create
        //public IActionResult Create()
        //{
        //    var model = new AppointmentViewModel
        //    {
        //        Vehicles = _vehicleRepository.GetAll().ToList(),
        //        AppointmentTypes = _appointmentTypeRepository.GetAll().ToList()                
        //    };

        //    return View(model);
        //}



        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        //// GET: Appointments/Edit/5    //TODO: editar só na secretary
        ////public async Task<IActionResult> Edit(int? id)
        ////{
        ////    //if (id == null)
        ////    //{
        ////    //    return NotFound();
        ////    //}

        ////    //var appointment = await _context.Appointments.FindAsync(id);
        ////    //if (appointment == null)
        ////    //{
        ////    //    return NotFound();
        ////    //}
        ////    //ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", appointment.ClientId);
        ////    //ViewData["MecanicId"] = new SelectList(_context.Mecanics, "Id", "Id", appointment.MecanicId);
        ////    //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "LicensePlate", appointment.VehicleId);
        ////    return View(/*appointment*/);
        ////}

        ////// POST: Appointments/Edit/5
        ////// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        ////// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Time,WorkEstimate,Information,MecanicId,ClientId,VehicleId,IsConfirmed,IsUrgent")] Appointment appointment)
        ////{
        ////    //if (id != appointment.Id)
        ////    //{
        ////    //    return NotFound();
        ////    //}

        ////    //if (ModelState.IsValid)
        ////    //{
        ////    //    try
        ////    //    {
        ////    //        _context.Update(appointment);
        ////    //        await _context.SaveChangesAsync();
        ////    //    }
        ////    //    catch (DbUpdateConcurrencyException)
        ////    //    {
        ////    //        if (!AppointmentExists(appointment.Id))
        ////    //        {
        ////    //            return NotFound();
        ////    //        }
        ////    //        else
        ////    //        {
        ////    //            throw;
        ////    //        }
        ////    //    }
        ////    //    return RedirectToAction(nameof(Index));
        ////    //}
        ////    //ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", appointment.ClientId);
        ////    //ViewData["MecanicId"] = new SelectList(_context.Mecanics, "Id", "Id", appointment.MecanicId);
        ////    //ViewData["VehicleId"] = new SelectList(_context.Vehicles, "Id", "LicensePlate", appointment.VehicleId);
        ////    return View(appointment);
        ////}

        //// GET: Appointments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound(); //TODO: substituir error views
        //    }

        //    var appointment = await _appointmentRepository.GetByIdAsync(id.Value);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(appointment);
        //}

        //// POST: Appointments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var appointment = await _appointmentRepository.GetByIdAsync(id);
        //    await _appointmentRepository.DeleteAsync(appointment);
        //    return RedirectToAction(nameof(Index));
        //}

        ////private bool AppointmentExists(int id)
        ////{
        ////    return _appointmentRepository.ExistAsync(id);
        ////}



        //public class AppointmentData  //class for syncfusion scheduler
        //{
        //    public int Id { get; set; }
        //    public string Subject { get; set; }
        //    public DateTime StartTime { get; set; }
        //    public DateTime EndTime { get; set; }
        //}


        //public List<AppointmentData> GetScheduleData()
        //{
        //    List<AppointmentData> appData = new List<AppointmentData>();
        //    appData.Add(new AppointmentData
        //    {
        //        Id = 1,
        //        Subject = "Explosion of Betelgeuse Star",
        //        StartTime = new DateTime(2020, 8, 28, 9, 30, 0),
        //        EndTime = new DateTime(2020, 8, 30, 11, 0, 0)
        //    });
        //    appData.Add(new AppointmentData
        //    {
        //        Id = 2,
        //        Subject = "Thule Air Crash Report",
        //        StartTime = new DateTime(2018, 2, 12, 12, 0, 0),
        //        EndTime = new DateTime(2018, 2, 12, 14, 0, 0)
        //    });
        //    appData.Add(new AppointmentData
        //    {
        //        Id = 3,
        //        Subject = "Blue Moon Eclipse",
        //        StartTime = new DateTime(2018, 2, 13, 9, 30, 0),
        //        EndTime = new DateTime(2018, 2, 13, 11, 0, 0)
        //    });
        //    appData.Add(new AppointmentData
        //    {
        //        Id = 4,
        //        Subject = "Meteor Showers in 2018",
        //        StartTime = new DateTime(2018, 2, 14, 13, 0, 0),
        //        EndTime = new DateTime(2018, 2, 14, 14, 30, 0)
        //    });
        //    appData.Add(new AppointmentData
        //    {
        //        Id = 5,
        //        Subject = "Milky Way as Melting pot",
        //        StartTime = new DateTime(2018, 2, 15, 12, 0, 0),
        //        EndTime = new DateTime(2018, 2, 15, 14, 0, 0)
        //    });
        //    return appData;
        //}



    }
}

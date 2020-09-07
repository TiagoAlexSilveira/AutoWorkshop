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

namespace AutoWorkshop.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public ClientsController(IClientRepository clientRepository,
                                 IVehicleRepository vehicleRepository,
                                 IAppointmentRepository appointmentRepository)
        { 
            _clientRepository = clientRepository;
            _vehicleRepository = vehicleRepository;
            _appointmentRepository = appointmentRepository;
        }

        // GET: Clients
        public IActionResult Index()
        {

            var vehicles =  _vehicleRepository.GetAll().Include(b => b.Brand)
                                                       .Where(p => p.User.UserName == User.Identity.Name);
                                                      


            var appointments = _appointmentRepository.GetAll().Include(v => v.Vehicle)
                                                              .Include(c => c.Client)
                                                              .Where(p => p.Client.User.UserName == User.Identity.Name);


            var vmodel = new ClientIndexViewModel
            {
                Appointments = appointments.ToList(),
                Vehicles = vehicles.ToList()
            };

            return View(vmodel);
        }

    //    // GET: Clients/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var client = await _context.Clients
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (client == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(client);
    //    }

    //    // GET: Clients/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Clients/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,StreetAddress,PhoneNumber,PostalCode,DateofBirth,TaxIdentificationNumber,CitizenCardNumber")] Client client)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(client);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(client);
    //    }

    //    // GET: Clients/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var client = await _context.Clients.FindAsync(id);
    //        if (client == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(client);
    //    }

    //    // POST: Clients/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,StreetAddress,PhoneNumber,PostalCode,DateofBirth,TaxIdentificationNumber,CitizenCardNumber")] Client client)
    //    {
    //        if (id != client.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(client);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!ClientExists(client.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(client);
    //    }

    //    // GET: Clients/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var client = await _context.Clients
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (client == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(client);
    //    }

    //    // POST: Clients/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var client = await _context.Clients.FindAsync(id);
    //        _context.Clients.Remove(client);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool ClientExists(int id)
    //    {
    //        return _context.Clients.Any(e => e.Id == id);
    //    }
    }
}

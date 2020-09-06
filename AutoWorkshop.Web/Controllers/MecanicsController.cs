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
    public class MecanicsController : Controller
    {
        private readonly IRepairRepository _repairRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public MecanicsController(IRepairRepository repairRepository,
                                  IAppointmentRepository appointmentRepository)
        {
            _repairRepository = repairRepository;
            _appointmentRepository = appointmentRepository;
        }

        // GET: Mecanics
        public IActionResult Index()
        {
            //repairs do mecanico logado
            var repair = _repairRepository.GetAll().Include(m => m.Appointment)
               .Where(p => p.Appointment.Mecanic.User.Email == User.Identity.Name);

            //appointments do mecanico logado
            var appointments = _appointmentRepository.GetAll()
                .Where(m => m.Mecanic.User.Email == User.Identity.Name);

            var model = new RepairMecViewModel
            {
                Appointments = appointments
            };

            return View(model);
        }

        //// GET: Mecanics/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mecanic = await _context.Mecanics
        //        .Include(m => m.Specialty)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (mecanic == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(mecanic);
        //}

        //// GET: Mecanics/Create
        //public IActionResult Create()
        //{
        //    ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Id");
        //    return View();
        //}

        //// POST: Mecanics/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SpecialtyId,Id,FirstName,LastName,StreetAddress,PhoneNumber,PostalCode,DateofBirth,TaxIdentificationNumber,CitizenCardNumber")] Mecanic mecanic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(mecanic);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Id", mecanic.SpecialtyId);
        //    return View(mecanic);
        //}

        //// GET: Mecanics/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mecanic = await _context.Mecanics.FindAsync(id);
        //    if (mecanic == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Id", mecanic.SpecialtyId);
        //    return View(mecanic);
        //}

        //// POST: Mecanics/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("SpecialtyId,Id,FirstName,LastName,StreetAddress,PhoneNumber,PostalCode,DateofBirth,TaxIdentificationNumber,CitizenCardNumber")] Mecanic mecanic)
        //{
        //    if (id != mecanic.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(mecanic);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MecanicExists(mecanic.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SpecialtyId"] = new SelectList(_context.Specialty, "Id", "Id", mecanic.SpecialtyId);
        //    return View(mecanic);
        //}

        //// GET: Mecanics/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var mecanic = await _context.Mecanics
        //        .Include(m => m.Specialty)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (mecanic == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(mecanic);
        //}

        //// POST: Mecanics/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var mecanic = await _context.Mecanics.FindAsync(id);
        //    _context.Mecanics.Remove(mecanic);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MecanicExists(int id)
        //{
        //    return _context.Mecanics.Any(e => e.Id == id);
        //}
    }
}

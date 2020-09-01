using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    public class SecretariesController : Controller
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMecanicRepository _mecanicRepository;

        public SecretariesController(ISecretaryRepository secretaryRepository,
                                     IAppointmentRepository appointmentRepository,
                                     IMecanicRepository mecanicRepository)
        {
            _secretaryRepository = secretaryRepository;
            _appointmentRepository = appointmentRepository;
            _mecanicRepository = mecanicRepository;
        }


        public IActionResult Index()
        {
            var appointment = _appointmentRepository.GetAll().Include(v => v.Vehicle)
                                                             .Include(c => c.Client);

            var model = new SecretaryAppointmentViewModel
            {
                Appointments = appointment,
                Mechanics = _mecanicRepository.GetComboMecanics()
            };


            return View(model);
        }

        //TODO: Fazer o post ao adicionar um mecânico


        //// GET: Secretaries
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Secretaries.ToListAsync());
        //}

        //// GET: Secretaries/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var secretary = await _context.Secretaries
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (secretary == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(secretary);
        //}

        //// GET: Secretaries/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Secretaries/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,StreetAddress,PhoneNumber,PostalCode,DateofBirth,TaxIdentificationNumber,CitizenCardNumber")] Secretary secretary)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(secretary);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(secretary);
        //}

        //// GET: Secretaries/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var secretary = await _context.Secretaries.FindAsync(id);
        //    if (secretary == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(secretary);
        //}

        //// POST: Secretaries/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,StreetAddress,PhoneNumber,PostalCode,DateofBirth,TaxIdentificationNumber,CitizenCardNumber")] Secretary secretary)
        //{
        //    if (id != secretary.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(secretary);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SecretaryExists(secretary.Id))
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
        //    return View(secretary);
        //}

        //// GET: Secretaries/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var secretary = await _context.Secretaries
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (secretary == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(secretary);
        //}

        //// POST: Secretaries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var secretary = await _context.Secretaries.FindAsync(id);
        //    _context.Secretaries.Remove(secretary);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SecretaryExists(int id)
        //{
        //    return _context.Secretaries.Any(e => e.Id == id);
        //}
    }
}

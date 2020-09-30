using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtiesController(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }


        // GET: Specialties
        public IActionResult Index()
        {
            var specialty = _specialtyRepository.GetAll();
            return View(specialty);
        }


        // GET: Specialties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _specialtyRepository.GetByIdAsync(id.Value);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }


        // GET: Specialties/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Specialties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                await _specialtyRepository.CreateAsync(specialty);
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }


        // GET: Specialties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _specialtyRepository.GetByIdAsync(id.Value);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }


        // POST: Specialties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _specialtyRepository.UpdateAsync(specialty);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _specialtyRepository.ExistAsync(specialty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }


        // GET: Specialties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialty = await _specialtyRepository.GetByIdAsync(id.Value);
            if (specialty == null)
            {
                return NotFound();
            }

            return View(specialty);
        }


        // POST: Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            await _specialtyRepository.DeleteAsync(specialty);
            return RedirectToAction(nameof(Index));
        }
    }
}

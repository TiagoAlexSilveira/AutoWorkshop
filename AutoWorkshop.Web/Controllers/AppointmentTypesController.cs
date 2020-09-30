using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AppointmentTypesController : Controller
    {

        private readonly IAppointmentTypeRepository _appointmentTypeRepository;

        public AppointmentTypesController(IAppointmentTypeRepository appointmentTypeRepository)
        {

            _appointmentTypeRepository = appointmentTypeRepository;
        }



        // GET: AppointmentTypes
        public IActionResult Index()
        {
            var appointmenttype = _appointmentTypeRepository.GetAll().ToList();
            return View(appointmenttype);
        }



        // GET: AppointmentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmenttype = await _appointmentTypeRepository.GetByIdAsync(id.Value);
            if (appointmenttype == null)
            {
                return NotFound();
            }

            return View(appointmenttype);
        }



        // GET: AppointmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: AppointmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentType appointmentmentType)
        {
            if (ModelState.IsValid)
            {
                await _appointmentTypeRepository.CreateAsync(appointmentmentType);
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentmentType);
        }

        // GET: AppointmentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentType = await _appointmentTypeRepository.GetByIdAsync(id.Value);
            if (appointmentType == null)
            {
                return NotFound();
            }
            return View(appointmentType);
        }



        // POST: AppointmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentType appointmentType)
        {
            //if (id != appointmentType.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {

                    await _appointmentTypeRepository.UpdateAsync(appointmentType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _appointmentTypeRepository.ExistAsync(appointmentType.Id))
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
            return View(appointmentType);
        }



        // GET: AppointmentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointmentType = await _appointmentTypeRepository.GetByIdAsync(id.Value);
            if (appointmentType == null)
            {
                return NotFound();
            }

            return View(appointmentType);
        }



        // POST: AppointmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentType = await _appointmentTypeRepository.GetByIdAsync(id);
            await _appointmentTypeRepository.DeleteAsync(appointmentType);
            return RedirectToAction(nameof(Index));
        }

    }
}

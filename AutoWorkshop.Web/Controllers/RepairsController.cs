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
    public class RepairsController : Controller
    {
        
        private readonly IRepairRepository _repairRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public RepairsController(IRepairRepository repairRepository,
                                 IClientRepository clientRepository,
                                 IAppointmentRepository appointmentRepository)
        {
            
            _repairRepository = repairRepository;
            _clientRepository = clientRepository;
            _appointmentRepository = appointmentRepository;
        }


        // GET: Repairs
        public IActionResult Index()
        {
            var repair = _repairRepository.GetAll().Include(r => r.Appointment).ToList();
            return View(repair);
        }



        // GET: Repairs/Details/5
        public async Task<IActionResult> Details(int? id)
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



        // GET: Repairs/Create
        public IActionResult Create()
        {
            var model = new RepairViewModel
            {
                //TODO: fazer repairs para o admin
                //appointments do mecanico logado
                Appointments = _appointmentRepository.GetComboUserAppointment(User.Identity.Name)
            };

            return View(model);
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
                return RedirectToAction(nameof(Index));
            }
            
            return View(repair);
        }

        // GET: Repairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Repairs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Repair repair)
        {
            //if (id != repair.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    await _repairRepository.UpdateAsync(repair);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _repairRepository.ExistAsync(repair.Id))
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

            return View(repair);
        }

        // GET: Repairs/Delete/5
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

        // POST: Repairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repair = await _repairRepository.GetByIdAsync(id);
            await _repairRepository.DeleteAsync(repair);
            return RedirectToAction(nameof(Index));
        }

        //private bool RepairExists(int id)
        //{
        //    return _context.Repairs.Any(e => e.Id == id);
        //}
    }
}

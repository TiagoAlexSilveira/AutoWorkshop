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

namespace AutoWorkshop.Web.Controllers
{
    public class BrandsController : Controller
    {
        public IBrandRepository _brandRepository { get; }

        public BrandsController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }



        // GET: Brands
        public IActionResult Index()
        {
            return View(_brandRepository.GetAll());
        }



        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }



        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await _brandRepository.CreateAsync(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }



        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }



        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand brand)
        {
            //if (id != brand.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    await _brandRepository.UpdateAsync(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _brandRepository.ExistAsync(brand.Id))
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
            return View(brand);
        }



        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }



        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _brandRepository.GetByIdAsync(id);
            await _brandRepository.DeleteAsync(vehicle);
            return RedirectToAction(nameof(Index));
        }
    }
}

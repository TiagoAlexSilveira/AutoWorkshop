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
    public class AdminsController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;

        public AdminsController(IAdminRepository adminRepository,
                                IConverterHelper converterHelper,
                                IImageHelper imageHelper,
                                IUserHelper userHelper)
        {
            _adminRepository = adminRepository;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            var vmodel = new AdminIndexViewModel
            {
                AllCount = _adminRepository.GetAllCount()
            };

            return View(vmodel);
        }

        public IActionResult ssIndex()
        {
            return View(_adminRepository.GetAll());
        }


        //// GET: Admins
        //public IActionResult Index()
        //{
        //    return View(_adminRepository.GetAll());
        //}

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _adminRepository.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }


        public async Task<IActionResult> AdminDetails(int Id)
        {
            var admin = await _adminRepository.GetByIdAsync(Id);

            var model = _converterHelper.ToPersonEditViewModel(admin);

            return PartialView("_AdminDetailsPartial", model);
        }



        public IActionResult Create()
        {
            return RedirectToAction("CreateWithRole", "Account");
        }



        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _adminRepository.GetByIdAsync(id.Value);
            if (admin == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPersonEditViewModel(admin);

            return View(model);
        }

        // POST: Admins/Edit/5
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
                    var path = string.Empty;

                    if (model.ImageFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "People");
                    }

                    var admin = _converterHelper.ToAdminEdit(model, path);
                    
                    await _adminRepository.UpdateAsync(admin);

                    ViewBag.UserMessage = "User Sucessfully Updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _adminRepository.ExistAsync(model.Id))
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

        //// GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _adminRepository.GetByIdAsync(id.Value);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        //// POST: Admins/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _adminRepository.GetByIdAsync(id);
            var user = await _userHelper.GetUserByIdAsync(admin.UserId);

            await _adminRepository.DeleteAsync(admin);
            await _userHelper.DeleteUserAsync(user);

            return RedirectToAction("ssIndex", "Admins");
        }
    }
}

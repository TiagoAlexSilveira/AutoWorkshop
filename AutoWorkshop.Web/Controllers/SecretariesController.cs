using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    public class SecretariesController : Controller
    {
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMechanicRepository _mecanicRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IUserHelper _userHelper;

        public SecretariesController(ISecretaryRepository secretaryRepository,
                                     IAppointmentRepository appointmentRepository,
                                     IMechanicRepository mecanicRepository,
                                     IConverterHelper converterHelper,
                                     IImageHelper imageHelper, IUserHelper userHelper)
        {
            _secretaryRepository = secretaryRepository;
            _appointmentRepository = appointmentRepository;
            _mecanicRepository = mecanicRepository;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
            _userHelper = userHelper;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ssIndex()
        {
            return View(_secretaryRepository.GetAll());
        }


        public IActionResult UnconfirmedAppointments()
        {                              
            
            var unconfirmedAppointments = _appointmentRepository.GetAll().Include(v => v.Vehicle)
                                                                        .Include(c => c.Client)
                                                                        .Include(m => m.Mechanic)
                                                                        .ThenInclude(c => c.Specialty)
                                                                        .Where(p => p.IsConfirmed != true);
                                                                       
                                                                        

            var model = new SecUnconfAppointViewModel
            {
                UnconfirmedAppointments = unconfirmedAppointments,
                Mechanics = _mecanicRepository.GetComboMecanics()
            };

            return View(model);
        }



        public IActionResult ConfirmedAppointments()
        {
            var confirmedAppointments = _appointmentRepository.GetAll().Include(v => v.Vehicle)
                                                                        .Include(c => c.Client)
                                                                        .Include(m => m.Mechanic).ThenInclude(c => c.Specialty)
                                                                        .Include(a => a.AppointmentType)
                                                                        .Where(p => p.IsConfirmed == true);


            var model = new SecConfAppointViewModel
            {
                ConfAppointments = confirmedAppointments
            };

            return View(model);
        }


        public async Task<IActionResult> SecretaryDetails(int Id)
        {
            var secretary = await _secretaryRepository.GetByIdAsync(Id);

            var model = _converterHelper.ToPersonEditViewModel(secretary);

            return PartialView("_SecretaryDetailsPartial", model);
        }


        //public async Task<IActionResult> Update(int id)
        //{
        //    var appointment = await _appointmentRepository.GetByIdAsync(id);


        //    appointment.IsConfirmed = true;
        //    await _appointmentRepository.UpdateAsync(appointment);

        //    return RedirectToAction("UnconfirmedAppointments");

        //}


        //[HttpPost]
        //public async Task<JsonResult> Update(SecUnconfAppointViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var appointment = _converterHelper.ToAppointment(model);

        //        await _appointmentRepository.UpdateAsync(appointment);

        //        return Json(appointment);
        //    }

        //    return Json(false);
        //}





        //GET: Secretary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretary = await _secretaryRepository.GetByIdAsync(id.Value);
            if (secretary == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToPersonEditViewModel(secretary);

            return View(model);
        }






        //POST: Secretary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "People");
                }

                try
                {
                    var secretary = _converterHelper.ToSecretaryEdit(model, path);

                    await _secretaryRepository.UpdateAsync(secretary);

                    ViewBag.UserMessage = "User Sucessfully Updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _secretaryRepository.ExistAsync(model.Id))
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



        // GET: Secretary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secretary = await _secretaryRepository.GetByIdAsync(id.Value);
            if (secretary == null)
            {
                return NotFound();
            }

            return View(secretary);
        }



        // POST: Secretary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secretary = await _secretaryRepository.GetByIdAsync(id);
            var user = await _userHelper.GetUserByIdAsync(secretary.UserId);

            await _secretaryRepository.DeleteAsync(secretary);
            await _userHelper.DeleteUserAsync(user);

            return RedirectToAction("ssIndex", "Secretaries");
        }


        //TODO: Fazer o post ao adicionar um mecânico


        //// GET: Secretaries
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Secretaries.ToListAsync());
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

        
    }
}

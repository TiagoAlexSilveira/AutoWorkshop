using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;

        public VehiclesController(IVehicleRepository vehicleRepository,
                                  IBrandRepository brandRepository,
                                  IConverterHelper converterHelper,
                                  IUserHelper userHelper)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        // GET: Vehicles
        public IActionResult Index()
        {
            return View(_vehicleRepository.GetAll().Include(v => v.Brand).ToList());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            var vehicle = new VehicleViewModel
            {
                Brands = _brandRepository.GetComboBrands()
            };

            return View(vehicle);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel vehicle)
        {
            if (this.ModelState.IsValid)
            {
                //vehicle.User = await _userHelper.GetUserByEmailAsync("");

                await _vehicleRepository.AddBrandToVehicle(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }


        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var vehicle = await _brandRepository.GetByIdWithBrand(id.Value); //para associar a brand ao veiculo

            if (vehicle == null)
            {
                return NotFound();
            }

            var helper = _converterHelper.ToVehicleViewModel(vehicle);  // o objecto vehicle vem sem brand 
            helper.Brands = _brandRepository.GetComboBrands();
            helper.BrandId = vehicle.Brand.Id;

            return View(helper);
        }


        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel helper)   //tive um problema de nomes em que não me passava o viewmodel com o nome model (lmao)
        {                                                                //consigo passar o viewmodel mas só vem com o BrandId da ViewModel pois é o que está no hidden="Id"
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;

                    var help = _converterHelper.ToVehicle(helper, path, false);
                    help.Brand = await _brandRepository.GetByIdAsync(help.BrandId);    //depois tenho de associar a Brand através do BrandId da ViewModel

                    await _vehicleRepository.UpdateAsync(helper);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleRepository.ExistAsync(helper.Id))
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
            return View(/*vehicle*/ helper);
        }


        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            await _vehicleRepository.DeleteAsync(vehicle);
            return RedirectToAction(nameof(Index));
        }

    }
}

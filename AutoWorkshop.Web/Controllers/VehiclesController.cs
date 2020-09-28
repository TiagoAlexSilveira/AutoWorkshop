using AutoWorkshop.Web.Data;
using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.EJ2.Linq;
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
        private readonly IClientRepository _clientRepository;

        public VehiclesController(IVehicleRepository vehicleRepository,
                                  IBrandRepository brandRepository,
                                  IConverterHelper converterHelper,
                                  IUserHelper userHelper,
                                  IClientRepository clientRepository)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _clientRepository = clientRepository;
        }





        // GET: Vehicles
        public IActionResult Index()
        {

            var vehicles = _vehicleRepository.GetAll().Include(v => v.Brand).Include(u => u.Client);

            return View(vehicles);
                      
        }


        public async Task<IActionResult> GetVehicles(int Id)
        {
            var vehicle = await _brandRepository.GetByIdWithBrand(Id);
            
            return PartialView("_VehiclesPartial", vehicle);
        }



        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            vehicle.Brand = await _brandRepository.GetByIdAsync(vehicle.BrandId);
            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            return View(vehicle);
        }



        // GET: Vehicles/Create
        public IActionResult Create()
        {
            if (User.IsInRole("Client"))
            {
                var vmodel = new VehicleViewModel
                {
                    Brands = _brandRepository.GetComboBrands()
                };

                return View(vmodel);
            }
            else
            {
                var vmodel = new VehicleViewModel
                {
                    Brands = _brandRepository.GetComboBrands(),
                    Clients = _clientRepository.GetComboClients()
                };

                return View(vmodel);
            }
        }


        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel vmodel)
        {
            if (this.ModelState.IsValid)
            {
                if (User.IsInRole("Client"))
                {
                    var path = string.Empty;
                    var client = _clientRepository.GetClientByUserEmail(User.Identity.Name);

                    var vehicle = _converterHelper.ToVehicle(vmodel, path, true);
                    vehicle.ClientId = client.Id;

                    await _vehicleRepository.CreateAsync(vehicle);

                    return RedirectToAction("MyVehicles", "Clients");
                }
                else
                {
                    var path = string.Empty;
                    var cli = await _clientRepository.GetByIdAsync(vmodel.ClientId);

                    var vehicle = _converterHelper.ToVehicle(vmodel, path, true);
                    vehicle.ClientId = cli.Id;

                    await _vehicleRepository.CreateAsync(vehicle);

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(vmodel);
        }


        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _brandRepository.GetByIdWithBrand(id.Value); //para associar a brand ao veiculo

            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vmodel = _converterHelper.ToVehicleViewModel(vehicle);  // o objecto vehicle vem sem brand 
            vmodel.Brands = _brandRepository.GetComboBrands();
            vmodel.BrandId = vehicle.Brand.Id;

            return View(vmodel);
        }



        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel vmodel)   //tive um problema de nomes em que não me passava o viewmodel com o nome model (lmao)
        {                                                                //consigo passar o viewmodel mas só vem com o BrandId da ViewModel pois é o que está no hidden="Id"
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;
                    
                    var vehicle = _converterHelper.ToVehicle(vmodel, path, false);

                    await _vehicleRepository.UpdateAsync(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleRepository.ExistAsync(vmodel.Id))
                    {
                        return new NotFoundViewResult("VehicleNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }

                if (User.IsInRole("Client"))
                {
                    return RedirectToAction("MyVehicles","Clients");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vmodel);
        }




        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
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

            if (User.IsInRole("Client"))
            {
                return RedirectToAction("MyVehicles", "Clients");
            }
            return RedirectToAction(nameof(Index));
        }



        public IActionResult VehicleNotFound()
        {
            return View();
        }
    }
}

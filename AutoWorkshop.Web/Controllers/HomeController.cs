using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;
        private readonly ISecretaryRepository _secretaryRepository;

        public HomeController(IUserHelper userHelper, IClientRepository clientRepository,
                              ISecretaryRepository secretaryRepository)
        {
            _userHelper = userHelper;
            _clientRepository = clientRepository;
            _secretaryRepository = secretaryRepository;
        }


        public async Task<IActionResult> Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

                if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
                {
                    return View("AdminIndex");
                }
                if (await _userHelper.IsUserInRoleAsync(user, "Mecanic"))
                {
                    return View("MechanicIndex");
                }
                if (await _userHelper.IsUserInRoleAsync(user, "Secretary"))
                {
                    return RedirectToAction("Index", "Secretaries");
                }

                var client = _clientRepository.GetClientByUserEmail(User.Identity.Name);
                if (string.IsNullOrEmpty(client.StreetAddress))
                {
                    return View("InfoAfterLogin", client);
                }

            }

            return View();
        }




        //Fill Info After Login(for clients only)
        [HttpPost]
        public async Task<IActionResult> InfoAfterLogin(Client client)
        {            

            await _clientRepository.UpdateAsync(client);

            ViewBag.Message = "Your registration is now complete! Welcome to Penguin AutoWorkshop!";

            return View();

        }


        


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}

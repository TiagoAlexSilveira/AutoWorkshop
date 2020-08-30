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

        public HomeController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
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
                    return View("SecretaryIndex");
                }
            }
            
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

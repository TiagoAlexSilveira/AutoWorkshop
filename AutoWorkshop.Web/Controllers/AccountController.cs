using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoWorkshop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;

        public AccountController(IUserHelper userHelper,
                                 IConfiguration configuration,
                                 IMailHelper mailHelper,
                                 IClientRepository clientRepository,
                                 IAdminRepository adminRepository)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
        }


        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Vehicles"); //redireciona para esta action quando o login é successfull
                }
            }

            ModelState.AddModelError(string.Empty, "Login Failed!");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    user = new User
                    {
                        Email = model.Username,
                        UserName = model.Username,
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "The user could not be created");
                        return View(model);  //retornamos a view outra vez para o user não ter de preencher os campos de novo
                    }



                    var client = new Client
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        User = user
                    };

                    await _clientRepository.CreateAsync(client);

                    //await _userHelper.CheckRoleAsync("Customer");

                    //await _userHelper.AddUserToRoleAsync(user, "Customer");

                    var isRole = await _userHelper.IsUserInRoleAsync(user, "Customer");

                
                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);

                    if (!isRole)
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Customer");
                    }


                    _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Verify your email to finish signing up for Penguin AutoWorkshop</h1>" +
                       $"<br><br>Please confirm your email by using this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "Instructions to confirm your sign up have been sent to your email.";


               

                   


                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "The username is already registered");
            }

            return View(model);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();

        }


        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

            //var client = _clientRepository.GetClientByUserId(user.Id);

            var model = new ChangeUserViewModel();

            if (user != null)
            {
                
                if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
                {
                    var admin = _adminRepository.GetAdminByUserId(user.Id);

                    model.FirstName = admin.FirstName;
                    model.LastName = admin.LastName;
                    model.StreetAddress = admin.StreetAddress;
                    model.PhoneNumber = admin.PhoneNumber;
                    model.PostalCode = admin.PostalCode;
                    model.DateofBirth = admin.DateofBirth;
                    model.TaxIdentificationNumber = admin.TaxIdentificationNumber;
                    model.CitizenCardNumber = admin.CitizenCardNumber;

                }
                else if (await _userHelper.IsUserInRoleAsync(user, "Customer"))
                {
                    var client = _clientRepository.GetClientByUserId(user.Id);

                    model.FirstName = client.FirstName;
                    model.LastName = client.LastName;
                    model.StreetAddress = client.StreetAddress;
                    model.PhoneNumber = client.PhoneNumber;
                    model.PostalCode = client.PostalCode;
                    model.DateofBirth = client.DateofBirth;
                    model.TaxIdentificationNumber = client.TaxIdentificationNumber;
                    model.CitizenCardNumber = client.CitizenCardNumber;
                    
                }             

                return View(model);
            };

            return View(model);  //TODO: vai dar merda
            
        }


        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var client = _clientRepository.GetClientByUserId(user.Id);

                    client.FirstName = model.FirstName;
                    client.LastName = model.LastName;
                    //TODO: adicionar o resto das propriedades para mudar 
                  
                    try
                    {
                        await _clientRepository.UpdateAsync(client);

                        ViewBag.UserMessage = "User update";
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(model);
        }


        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    var response = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (response.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "user not found");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);   //onde é encriptado

                        var token = new JwtSecurityToken(
                            _configuration["Token:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),  //quanto tempo demora a expirar o token
                            signingCredentials: credentials
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created(string.Empty, results);   //devolver o token criado
                    }
                }
            }

            return BadRequest();
        }
    }
}

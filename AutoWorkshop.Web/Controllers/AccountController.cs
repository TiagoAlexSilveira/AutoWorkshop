using AutoWorkshop.Web.Data.Entities;
using AutoWorkshop.Web.Data.Repositories;
using AutoWorkshop.Web.Helpers;
using AutoWorkshop.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Syncfusion.EJ2.DropDowns;
using System;
using System.Collections.Generic;
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
        private readonly IConverterHelper _converterHelper;
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IMechanicRepository _mechanicRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IImageHelper _imageHelper;

        public AccountController(IUserHelper userHelper, IConfiguration configuration,
                                 IMailHelper mailHelper, IClientRepository clientRepository,
                                 IAdminRepository adminRepository, IConverterHelper converterHelper,
                                 ISecretaryRepository secretaryRepository, IMechanicRepository mechanicRepository,
                                 ISpecialtyRepository specialtyRepository, IImageHelper imageHelper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailHelper = mailHelper;
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
            _converterHelper = converterHelper;
            _secretaryRepository = secretaryRepository;
            _mechanicRepository = mechanicRepository;
            _specialtyRepository = specialtyRepository;
            _imageHelper = imageHelper;
        }


        public IActionResult Login()
        {

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

                    return RedirectToAction("Index", "Home"); //redireciona para esta action quando o login é successfull
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


                    var isRole = await _userHelper.IsUserInRoleAsync(user, "Client");

                
                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);

                    if (!isRole)
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Client");
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

            if (user != null)
            {               
                if (await _userHelper.IsUserInRoleAsync(user, "Admin"))  
                {
                    var admin = _adminRepository.GetAdminByUserId(user.Id);

                    var model = _converterHelper.ToChangeUserViewModelAdmin(admin);

                    return View(model);
                }
                else if (await _userHelper.IsUserInRoleAsync(user, "Client"))
                {
                    var client = _clientRepository.GetClientByUserId(user.Id);

                    var model = _converterHelper.ToChangeUserViewModelClient(client);

                    return View(model);
                }    
                else if(await _userHelper.IsUserInRoleAsync(user, "Secretary"))
                {
                    var secretary = _secretaryRepository.GetSecretaryByUserId(user.Id);

                    var model = _converterHelper.ToChangeUserViewModelSecretary(secretary);

                    return View(model);
                }
                else if(await _userHelper.IsUserInRoleAsync(user, "Mecanic"))
                {
                    var mecanic = _mechanicRepository.GetMecanicByUserId(user.Id);

                    var model = _converterHelper.ToChangeUserViewModelMecanic(mecanic);

                    return View(model);
                }

                return View();
            };

            return View();  
        }


        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "People");
                }

                var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    if (await _userHelper.IsUserInRoleAsync(user, "Admin"))  
                    {
                        var admin = _converterHelper.ToAdmin(model, path);
                        await _adminRepository.UpdateAsync(admin);

                        ViewBag.UserMessage = "User updated";
                    }
                    else if (await _userHelper.IsUserInRoleAsync(user, "Client"))
                    {
                        var client = _converterHelper.ToClient(model, path);
                        await _clientRepository.UpdateAsync(client);

                        ViewBag.UserMessage = "User updated";
                    }
                    else if (await _userHelper.IsUserInRoleAsync(user, "Secretary"))
                    {
                        var secretary = _converterHelper.ToSecretary(model, path);
                        await _secretaryRepository.UpdateAsync(secretary);

                        ViewBag.UserMessage = "User updated";
                    }
                    else if (await _userHelper.IsUserInRoleAsync(user, "Mecanic"))
                    {
                        var mecanic = _converterHelper.ToMecanic(model, path);
                        await _mechanicRepository.UpdateAsync(mecanic);

                        ViewBag.UserMessage = "User updated";
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


        public IActionResult RecoverPassword()
        {
            return View();
        }



        public IActionResult ResetPassword(string token)
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password reset successful.";
                    return View();
                }

                this.ViewBag.Message = "Error while resetting the password.";
                return View(model);
            }

            this.ViewBag.Message = "User not found.";
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email); //verifica se user existe
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "The email doesn't correspont to a registered user.");
                    return this.View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user); //gera token

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                //mandar token através de email
                _mailHelper.SendMail(model.Email, "Penguin AutoWorkshop Password Reset", $"<h1>Penguin AutoWorkshop Password Reset</h1>" +
                $"To reset your account password click on this link:</br></br>" +
                $"<a href = \"{link}\">Reset Password</a>");
                this.ViewBag.Message = "A password email reset was sent to your email address, follow the directions" +
                    "in the email to reset your password.";  //TODO: em vez de viewbag, aparecer outra view, com esta mensagem
                return this.View();

            }

            return this.View(model);
        }


        public IActionResult CreateWithRole()
        {
            var model = new CreateAccountViewModel
            {
                Roles = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Client", Value = "Client" },
                    new SelectListItem { Text = "Secretary", Value = "Secretary" },
                    new SelectListItem { Text = "Mechanic", Value = "Mechanic" },
                    new SelectListItem { Text = "Admin", Value = "Admin" },
                },

                Specialties = _specialtyRepository.GetComboSpecialty()

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWithRole(CreateAccountViewModel model)
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

                    if (model.Role == "Client")
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Client");
                        var client = _converterHelper.ToClientCreate(model);
                        client.User = user;

                        await _clientRepository.CreateAsync(client);
                    }
                    else if (model.Role == "Secretary")
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Secretary");
                        var secret = _converterHelper.ToSecretaryCreate(model);
                        secret.User = user;

                        await _secretaryRepository.CreateAsync(secret);
                    }
                    else if (model.Role == "Admin")
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Admin");
                        var admin = _converterHelper.ToAdminCreate(model);
                        admin.User = user;

                        await _adminRepository.CreateAsync(admin);
                    }
                    else if (model.Role == "Mechanic")
                    {
                        await _userHelper.AddUserToRoleAsync(user, "Mechanic");
                        var mecha = _converterHelper.ToMechanicCreate(model);
                        mecha.SpecialtyId = model.SpecialtyId;
                        mecha.User = user;

                        await _mechanicRepository.CreateAsync(mecha);
                    }

                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                    var tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken,
                    }, protocol: HttpContext.Request.Scheme);


                    _mailHelper.SendMail(model.Username, "Email confirmation", $"<h1>Verify your email to finish signing up for Penguin AutoWorkshop</h1>" +
                       $"<br><br>Please confirm your email by using this link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");
                    this.ViewBag.Message = "Account Creation Sucessfull";


                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "The username is already registered");
                return View(model);
            }

            return View(model);
        }
    }
}

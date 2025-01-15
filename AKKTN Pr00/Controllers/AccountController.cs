
using AKKTN_Pr00.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AKKTN_Pr00.Models;
using DocumentFormat.OpenXml.InkML;
using AKKTN_Pr00.Data;

namespace AKKTN_Pr00.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<user>? signInManager;
        private readonly UserManager<user>? userManager;
        private readonly AppDBContext _context;

        public AccountController(SignInManager<user>? signInManager, UserManager<user>? userManager,AppDBContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _context = context;
        }

        public IActionResult Login()
        {
          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Sign_in_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var findemail = _context.companies
                   .FirstOrDefault(ad => ad.EmailAddress1.Equals(companyemail) && ad.companypassword.Equals(companypassword));

                //var result = await signInManager.PasswordSignInAsync(model.EmailAddress1, model.companypassword, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("CompanyID,CompanyName,companypassword,RegistrationNumber,Status,ContactName1,EmailAddress1,Cellphone1,ContactName2,EmailAddress2,Cellphone2")] Sign_UpViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Sign_in_ViewModel);
                await _context.SaveChangesAsync();


                user user = new user 
                {
                
                    FullName = model.CompanyName,
                    UserName = model.EmailAddress1,
                    Email = model.EmailAddress1


                 };

                var result = await userManager.CreateAsync(user , model.companypassword);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);
        }
        public IActionResult VerifyEmail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something is wrong!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
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
                var user = await userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, model.NewPassword);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email not found!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }

}


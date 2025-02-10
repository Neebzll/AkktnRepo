
using AKKTN_Pr00.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AKKTN_Pr00.Models;
using DocumentFormat.OpenXml.InkML;
using AKKTN_Pr00.Data;
using System.Text;
using System.Security.Cryptography;
using System.Reflection.Metadata.Ecma335;

namespace AKKTN_Pr00.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<user>? signInManager;
        private readonly UserManager<user>? userManager;
        private readonly AppDBContext _context;

        public AccountController(SignInManager<user>? signInManager, UserManager<user>? userManager, AppDBContext context)
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
        public IActionResult Login(Sign_in_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.companypassword = hashpassword(model.companypassword);
                var findemail = _context.companies
                    .FirstOrDefault(ad =>
                        (ad.Email1.Equals(model.EmailAddress1) || ad.Email2.Equals(model.EmailAddress1))
                        && ad.companypass.Equals(model.companypassword));

                if (findemail != null)
                {

                    HttpContext.Session.SetString("isAdmin", "false");
                    HttpContext.Session.SetString("Signed", model.EmailAddress1);
                    HttpContext.Session.SetString("companyID", findemail.CompanyID);

                    return RedirectToAction("Index", "Clients", new { id = findemail.CompanyID });
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            return View(model);

        }
        private static string GenerateCompanyId(string CompanyName, string cellphone1)
        {
            // Combine the company name and cell
            string combined = $"{CompanyName.Trim().ToLower()}-{cellphone1.Trim()}";

            // Generate a hash of the combined string
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                // Convert the byte array to a hexadecimal string
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                // Return the first 10 characters of the hash as the Company ID
                return hashString.ToString().Substring(0, 10).ToUpper();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        public string hashpassword(string password)
        {
            SHA256 sha256 = SHA256.Create();
            var bytes = Encoding.Default.GetBytes(password);
            var hashed = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashed);
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("CompanyName,companypassword,confirmcompanypassword,RegistrationNumber,status,ContactName1,EmailAddress1,Cellphone1,ContactName2,EmailAddress2,Cellphone2")] Sign_UpViewModel model)
        {
            if (ModelState.IsValid)
            {
                string ID = "";

                ID = GenerateCompanyId(model.CompanyName, model.Cellphone1); ;

                string hashedpass = hashpassword(model.companypassword);
                Company com = new Company()
                {
                    CompanyID = ID,
                    CompanyName = model.CompanyName,
                    companypass = hashedpass,
                    ContactName1 = model.ContactName1,
                    Email1 = model.EmailAddress1,
                    Cell1 = model.Cellphone1,
                    ContactName2 = model.ContactName2,
                    Cell2 = model.Cellphone2,
                    Email2 = model.EmailAddress2,
                    RegistrationNumber = model.RegistrationNumber,
                    Status = model.status

                };

                _context.companies.Add(com);
                await _context.SaveChangesAsync();


                //user user = new user 
                //{

                //    FullName = model.CompanyName,
                //    UserName = model.EmailAddress1,
                //    Email = model.EmailAddress1


                // };

                //var result = await userManager.CreateAsync(user , model.companypassword);

                //await signInManager.SignInAsync(newUser, isPersistent: false);
                return RedirectToAction("Login", "Account");


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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("Login", "Account");
        }

    }

}



//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using AKKTN_Pr00.Data;
//using AKKTN_Pr00.Models;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
//using AKKTN_Pr00.ViewModel;
//using System.Text;
//using System.Security.Cryptography;
//using Auth0.AspNetCore.Authentication;

//public class AccountController : Controller
//{
//    private readonly SignInManager<user>? signInManager;
//    private readonly UserManager<user>? userManager;
//    private readonly AppDBContext _context;

//    public AccountController(SignInManager<user>? signInManager, UserManager<user>? userManager, AppDBContext context)
//    {
//        this.signInManager = signInManager;
//        this.userManager = userManager;
//        _context = context;
//    }

//    public IActionResult Login()
//    {
//        return View();
//    }

//    [HttpPost]
//    public async Task<IActionResult> Login(Sign_in_ViewModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            model.companypassword = hashpassword(model.companypassword);
//            var findemail = _context.companies
//                .FirstOrDefault(ad =>
//                    (ad.Email1.Equals(model.EmailAddress1) || ad.Email2.Equals(model.EmailAddress1))
//                    && ad.companypass.Equals(model.companypassword));

//            if (findemail != null)
//            {
//                HttpContext.Session.SetString("isAdmin", "false");
//                HttpContext.Session.SetString("Signed", model.EmailAddress1);
//                HttpContext.Session.SetString("companyID", findemail.CompanyID);

//                return RedirectToAction("Index", "Clients", new { id = findemail.CompanyID });
//            }
//            else
//            {
//                ModelState.AddModelError("", "Email or password is incorrect.");
//                return View(model);
//            }
//        }
//        return View(model);
//    }

//    // 🔹 Auth0 Login (Redirect to Auth0)
//    public async Task LoginWithAuth0(string returnUrl = "/")
//    {
//        //var authenticationProperties = new logina { RedirectUri = returnUrl };
//        var authenticationProperties = new LoginAuthenticationPropertiesBuilder ()
//            .WithRedirectUri(returnUrl)
//            .Build ();
//        await HttpContext.ChallengeAsync (Auth0Constants.AuthenticationScheme,authenticationProperties);
//    }

//    // 🔹 Auth0 Callback (Handles Auth0 login response)
//    public async Task<IActionResult> Auth0Callback()
//    {
//        var result = await HttpContext.AuthenticateAsync();
//        if (!result.Succeeded)
//        {
//            return RedirectToAction("Login", "Account");
//        }

//        var claims = result.Principal.Claims;
//        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

//        if (!string.IsNullOrEmpty(email))
//        {
//            var existingUser = _context.companies.FirstOrDefault(c => c.Email1 == email || c.Email2 == email);
//            if (existingUser != null)
//            {
//                HttpContext.Session.SetString("Signed", email);
//                HttpContext.Session.SetString("companyID", existingUser.CompanyID);
//                return RedirectToAction("Index", "Clients", new { id = existingUser.CompanyID });
//            }
//        }

//        return RedirectToAction("Login", "Account");
//    }

//    // 🔹 Auth0 Logout
//    public async Task<IActionResult> Logout()
//    {
//        HttpContext.Session.Clear();
//        await HttpContext.SignOutAsync();
//        return RedirectToAction("Login", "Account");
//    }

//    public string hashpassword(string password)
//    {
//        SHA256 sha256 = SHA256.Create();
//        var bytes = Encoding.Default.GetBytes(password);
//        var hashed = sha256.ComputeHash(bytes);
//        return Convert.ToBase64String(hashed);
//    }
//}
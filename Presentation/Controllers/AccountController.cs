using DataAccess.Models.IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Helpers;
using Presentation.Utilities;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager,
        IMailService _mailService , ISmsService _smsService)
        : Controller
    {


        #region Register

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);

            var user = new ApplicationUser
            {
                UserName = viewModel.UserName,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
            };

            var result = _userManager.CreateAsync(user, viewModel.Password).Result;
            if (result.Succeeded)
                return RedirectToAction("LogIn");

            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }

        }
        #endregion


        #region Login

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {

            if (!ModelState.IsValid) return View(viewModel);
            var user = _userManager.FindByEmailAsync(viewModel.Email).Result;

            if (user is not null)
            {
                bool flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if (flag)
                {
                    var result = _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account is not allowed");
                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is Locked Out");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(viewModel);

        }

        #endregion


        [HttpGet]
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction(nameof(Login));
        }


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword() => View();


        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action
                    (
                        nameof(ResetPassword),
                        "Account",
                        new { email = viewModel.Email, Token },
                        Request.Scheme
                    );
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink
                    };
                    //EmailSettings.SendEmail(email);
                    _mailService.send(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);
        }


        [HttpPost]
        public IActionResult SendResetPasswordLinkSms(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action
                    (
                        nameof(ResetPassword),
                        "Account",
                        new { email = viewModel.Email, Token },
                        Request.Scheme
                    );
                    var sms = new SmsMessage()
                    {
                        Body = ResetPasswordLink,
                        PhoneNumber = user.PhoneNumber,
                    };
                    _smsService.SendSms(sms);
                    return Ok("Check Your Sms");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);
        }



        [HttpGet]
        public IActionResult CheckYourInbox() => View();


        [HttpGet]
        public IActionResult ResetPassword(string email, string Token)
        {
            TempData["email"] = email;
            TempData["Token"] = Token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            string email = TempData["email"] as string ?? "";
            string Token = TempData["Token"] as string ?? "";

            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is not null)
            {
                var result = _userManager.ResetPasswordAsync(user, Token, viewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(nameof(ResetPassword), viewModel);
        }

        #endregion


        #region External Login with Google

        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(prop , GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                 claim.Issuer,
                 claim.OriginalIssuer,
                 claim.Type,
                 claim.Value
            });

            return RedirectToAction("Index" , "Home");
        }

     
        #endregion

    }
}

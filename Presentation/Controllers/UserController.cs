using Application.Users.Commands.RegisterPatronAndWallet;
using Application.Users.Commands.RegisterUser;
using Application.Users.Queries.LoginUser;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Extensions;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly ISender sender;

        public UserController (ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new LoginUserQuery { ReturnUrl = returnUrl });
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserQuery model, string returnUrl) {
            var claimIdentity = await sender.Send(model);
            if (claimIdentity.IsSuccess) {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimIdentity.Value()));
                return Redirect(returnUrl ?? "/");
            }
            else {
                var validationResult = (IValidationResult)claimIdentity;

                ModelState.AddModelError(validationResult.Errors);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration(string returnUrl) {
            if (!User.Identity.IsAuthenticated) {
                return View(new RegisterPatronAndWalletCommand { 
                    RegisterUserCommand = new RegisterUserCommand { ReturnUrl = returnUrl }
                });
            }
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(RegisterPatronAndWalletCommand model, string returnUrl) {
            var claimIdentity = await sender.Send(model);
            if (claimIdentity.IsSuccess) {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimIdentity.Value()));
                return Redirect(returnUrl ?? "/");
            }
            else {
                var validationResult = (IValidationResult)claimIdentity;

                ModelState.AddModelError(validationResult.Errors);
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "NewsFeed");
        }
    }
}

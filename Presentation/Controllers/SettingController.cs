using Application.Authors.Commands.ChangeAuthorAndUserSettings;
using Application.Authors.Commands.CreateAuthorAndChagneUserRole;
using Application.Authors.Queries.GetAuthor;
using Application.Users.Queries.GetUser;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Controllers.Extensions;
using Presentation.ViewModels;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SettingController : Controller
    {
        private readonly ISender sender;
        private readonly LinkGenerator linkGenerator;

        public SettingController(ISender sender, LinkGenerator linkGenerator) {
            this.sender = sender;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index() {
            var UserId = Guid.Parse(User.FindFirstValue("Id"));

            if (User.IsInRole("Author")) {
                var authorResult = await sender.Send(new GetAuthorQuery(UserId));
                if (authorResult.IsSuccess) {
                    return View(new SettingViewModel
                    (
                        SettingUserViewModel.Create(authorResult.Value())
                    ));
                }
            }
            else{
                var patronResult = await sender.Send(new GetUserQuery(UserId));
                if (patronResult.IsSuccess) {
                    return View(new SettingViewModel
                    (
                        SettingUserViewModel.Create(patronResult.Value())
                    ));
                }
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> RegisterAuthor(SettingViewModel model) {
            var command = new CreateAuthorAndChagneUserRoleCommand
            {
                CreateAuthorCommand = model.CreateAuthorCommand
            };
            var claimsIdentity = await sender.Send(command);

            await HttpContext.SignOutAsync();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity.Value()));

            return Redirect("Index");
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangeSettings(SettingViewModel model) {
            if (User.IsInRole("Author")) {
                var result = await sender.Send(new ChangeAuthorAndUserSettingsCommand
                {
                    ChangeUserSettingsCommand = model.ChangeUserSettingsCommand,
                    ChangeAuthorSettingsCommand = model.ChangeAuthorSettingsCommand
                });

                if (result.IsFailure) {
                    var validationResult = (IValidationResult)result;

                    ModelState.AddModelError(validationResult.Errors);
                }
            }
            else {
                var result = await sender.Send(model.ChangeUserSettingsCommand!);
                
                if (result.IsSuccess && result.Value()) {
                    await HttpContext.SignOutAsync();

                    var path = linkGenerator.GetPathByAction("Index", "Setting")!;

                    return RedirectToAction("Login", "User", new{ returnUrl = path});
                }
                else if (result.IsFailure) {
                    var validationResult = (IValidationResult)result;

                    ModelState.AddModelError(validationResult.Errors);
                }
            }

            if (User.IsInRole("Author")) {
                var authorResult = await sender.Send(new GetAuthorQuery(model.ChangeUserSettingsCommand.UserId));
                
                if (authorResult.IsSuccess) {
                    return View("Index", new SettingViewModel
                    (
                        SettingUserViewModel.Create(authorResult.Value())
                    ));
                }
            }
            else {
                var patronResult = await sender.Send(new GetUserQuery(model.ChangeUserSettingsCommand.UserId));
                
                if (patronResult.IsSuccess) {
                    return View("Index",new SettingViewModel
                    (
                        SettingUserViewModel.Create(patronResult.Value())
                    ));
                }
            }

            return RedirectToAction("Index");
        }
    }
}

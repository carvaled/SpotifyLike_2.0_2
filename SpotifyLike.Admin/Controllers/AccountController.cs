using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Spotify.Application.Admin.Interfaces;
using Spotify.Application.Shared.Dto;

namespace SpotifyLike.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IContaAdminAuthService authenticationService;

        public AccountController(IContaAdminAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SingIn(LoginDto dto)
        {
            if (ModelState is { IsValid: false })
                return View("Index");

            try
            {
                var accountDto = authenticationService.Authentication(dto);
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, accountDto.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, accountDto.Nome));
                identity.AddClaim(new Claim(ClaimTypes.Email, accountDto.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, accountDto.PerfilType.ToString()));

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = "/account"
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                if (ex is ArgumentException argEx)
                {
                    ModelState.AddModelError("login_failed", argEx.Message);
                }
                else
                    ViewBag.Alert = "Ocorreu um erro ao realizar login.";
                return View("Index");
            }
        }

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Index");
        }
    }
}
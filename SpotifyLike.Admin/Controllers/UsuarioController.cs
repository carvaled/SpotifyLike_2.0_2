using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotify.Application.Admin;
using Spotify.Application.Admin.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SpotifyLike.Admin.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private Guid UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated && Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId))
                {
                    return userId;
                }
                throw new ArgumentNullException();
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            try
            {
                var user = UserId.ToString();
            }
            catch
            {
                ViewBag.LoginError = "Usuário sem permissão de acesso.";
                HttpContext.SignOutAsync();
            }
        }

        private ContaAdminService Services { get; set; }
        public UsuarioController(ContaAdminService service)
        {
            Services = service;
        }

        public IActionResult Index()
        {
            var data = this.Services.FindAll();
            return View(data);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Save(ContaAdminDto dto)
        {
            if (ModelState is { IsValid: false })
                return this.Create();

            try
            {
                dto.UsuarioId = UserId;
                this.Services.Create(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = "Ocorreu um erro ao salvar os dados do usuário.";
                return this.Create();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ContaAdminDto dto)
        {
            if (ModelState is { IsValid: false })
                return View("Edit");

            try
            {
                dto.UsuarioId = UserId;
                this.Services.Update(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = $"Ocorreu um erro ao atualizar os dados do usuário {dto?.Nome}.";
                return View("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid Id)
        {
            try
            {
                var result = this.Services.FindById(Id);
                return View(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;

                else
                    ViewBag.Alert = "Ocorreu um erro ao editar os dados deste usuário.";
            }
            return View("Index");
        }


        public IActionResult Delete(ContaAdminDto dto)
        {
            try
            {
                dto.UsuarioId = UserId;
                var result = this.Services.Delete(dto);
                if (result)
                    ViewBag.Alert = $"Usuário {dto?.Nome} excluído.";
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = $"Ocorreu um erro ao excluir o usuário {dto?.Nome}.";
            }
            return View("Index");
        }
    }
}
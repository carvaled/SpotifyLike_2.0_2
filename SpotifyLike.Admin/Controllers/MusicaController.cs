using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotify.Application.Streaming;
using Spotify.Application.Streaming.Dto;
using Spotify.Admin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace SpotifyLike.Admin.Controllers
{
    public class MusicaController : Controller
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

        private MusicaService Services { get; set; }
        private AlbumService AlbumService { get; set; }
        public MusicaController(MusicaService musicaService, AlbumService albumService)
        {
            Services = musicaService;
            AlbumService = albumService;
        }

        public virtual IActionResult Index()
        {
            var data = this.Services.FindAll();
            return View(data);
        }

        [Authorize]
        public IActionResult Create()
        {
            var albuns = this.AlbumService.FindAll();
            var viewModel = new MusicViewModel
            {
                Music = new(),
                Albums = albuns
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save(MusicViewModel viewModel)
        {
            if (ModelState is { IsValid: false })
                return Index();

            try
            {
                viewModel.Music.UsuarioId = UserId;
                this.Services.Create(viewModel.Music);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = "Ocorreu um erro ao salvar os dados da musica.";
                return Index();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(MusicViewModel viewModel)
        {
            if (ModelState is { IsValid: false })
                return View("Edit");

            try
            {
                viewModel.Music.UsuarioId = UserId;
                this.Services.Update(viewModel.Music);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = "Ocorreu um erro ao atualizar a musica {viewModel.Music?.Name}.";
                return View("Edit");
            }
        }

        [Authorize]
        public IActionResult Edit(Guid Id)
        {
            try
            {
                var result = this.Services.FindById(Id);
                var albuns = this.AlbumService.FindAll();
                var viewModel = new MusicViewModel
                {
                    Music = result,
                    Albums = albuns
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = "Ocorreu um erro ao editar os dados desta musica.";
            }
            var data = this.Services.FindAll();
            return View("Index", data);
        }

        [Authorize]
        public IActionResult Delete(MusicaDto dto)
        {
            try
            {
                var result = this.Services.Delete(dto);
                if (result)
                    ViewBag.Alert = $"Musica {dto?.Nome} excluída.";
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;

                else
                    ViewBag.Alert = $"Ocorreu um erro ao excluir a musica {dto?.Nome}.";
            }
            var data = this.Services.FindAll();
            return View("Index", data);
        }
    }
}
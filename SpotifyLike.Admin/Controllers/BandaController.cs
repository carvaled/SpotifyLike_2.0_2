using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Spotify.Application.Streaming;
using Spotify.Application.Streaming.Dto;

namespace SpotifyLike.Admin.Controllers
{
    public class BandaController : Controller
    {
        private BandaService Services { get; set; }

        public BandaController(BandaService services)  
        {
            this.Services = services;
        }

        public IActionResult Index()
        {
            var bandas = this.Services.FindAll();
            return View(bandas);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new BandaDto());
        }


        [HttpPost]
        [Authorize]
        public IActionResult Save(BandaDto dto)
        {
            if (ModelState is { IsValid: false })
                return View("Index");

            try
            {
                this.Services.Criar(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = "Ocorreu um erro ao salvar os dados da banda.";
                return Index();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(BandaDto dto)
        {
            if (ModelState is { IsValid: false })
                return View("Edit");

            try
            {
                this.Services.Update(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;
                else
                    ViewBag.Alert = $"Ocorreu um erro ao atualizar a banda.";
                return View("Edit");
            }
        }

        [Authorize]
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
                    ViewBag.Alert = "Ocorreu um erro ao editar os dados desta banda.";
            }
            var data = this.Services.FindAll();
            return View("Index", data);
        }

        [Authorize]
        public IActionResult Delete(BandaDto dto)
        {
            try
            {
                var result = this.Services.Delete(dto);
                if (result)
                    ViewBag.Alert = $"Banda excluída.";
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException argEx)
                    ViewBag.Alert = argEx.Message;

                else
                    ViewBag.Alert = $"Ocorreu um erro ao excluir a banda.";
            }
            var data = this.Services.FindAll();
            return View("Index", data);
        }
    }
}
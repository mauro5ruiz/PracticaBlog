using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (slider.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var filesStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(filesStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(slider);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Slider slider = new Slider();
            slider = _contenedorTrabajo.Slider.Get(id);
            if (slider==null)
            {
                return NotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _webHostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var articuloDesdeBd = _contenedorTrabajo.Slider.Get(slider.Id);

                if (archivos.Count > 0)
                {
                    //Editaremos la imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);
                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos nuevamente el archivo
                    using (var filesStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(filesStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + nuevaExtension;
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //cuando la imagen ya existe y no se reemplaza debe conservar la foto que ya está en la base de datos
                    slider.UrlImagen = articuloDesdeBd.UrlImagen;
                }
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        #region
        public IActionResult Delete(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Slider.Get(id);
            var rutaDirectorioPrincipal = _webHostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            if (articuloDesdeBd == null)
            {
                return Json(new { success = false, message = "Error al eliminar el slider" });
            }
            _contenedorTrabajo.Slider.Remove(articuloDesdeBd);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider eliminado correctamente de la base de datos..." });
        }

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        #endregion
    }
}

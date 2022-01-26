using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models.ViewModels;
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
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM articuloVM = new ArticuloVM
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (articuloVM.Articulo.Id == 0)
                {
                    //Será un articulo nuevo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var filesStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(filesStreams);
                    }
                    articuloVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    articuloVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.Articulo.Add(articuloVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            articuloVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(articuloVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM articuloVM = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };
            if (id != null)
            {
                articuloVM.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }
            return View(articuloVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(articuloVM.Articulo.Id);

                if (archivos.Count > 0)
                {
                    //Editaremos la imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
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
                    articuloVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtension;
                    articuloVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.Articulo.Update(articuloVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //cuando la imagen ya existe y no se reemplaza debe conservar la foto que ya está en la base de datos
                    articuloVM.Articulo.UrlImagen = articuloDesdeBd.UrlImagen;
                }
                articuloVM.Articulo.FechaCreacion = articuloDesdeBd.FechaCreacion;
                _contenedorTrabajo.Articulo.Update(articuloVM.Articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpDelete]

        #region Llamada a las APIS
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            var rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeBd.UrlImagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }
            if (articuloDesdeBd == null)
            {
                return Json(new { success = false, message = "Error al eliminar el artículo" });
            }
            _contenedorTrabajo.Articulo.Remove(articuloDesdeBd);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Artículo eliminado correctamente de la base de datos..." });
        }

        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }
        #endregion
    }
}

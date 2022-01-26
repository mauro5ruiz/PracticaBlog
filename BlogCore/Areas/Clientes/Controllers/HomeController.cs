using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Controllers
{
    [Area("Clientes")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                Articulos = _contenedorTrabajo.Articulo.GetAll()
            };
            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Articulo.GetFirstOnDefault(a => a.Id == id);
            return View(articuloDesdeBd);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

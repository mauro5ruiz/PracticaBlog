using BlogCore.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult Index()
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Envío de Emial", "mauro5ruiz@hotmail.com"));
            mensaje.To.Add(new MailboxAddress("Test enviado", "mauro5ruiz@hotmail.com"));
            mensaje.Subject = "Sistema Blog Core ASP.NET CORE";
            mensaje.Body = new TextPart("plain")
            {
                Text = "Hola, somos BlogCore, un sistema diseñado por el Técnico Mauro Ruiz"
            };
            //using (var cliente= new SmtpClient())
            //{
            //    cliente.Connect("smtp.gmail.com", 465);
            //    cliente.Authenticate();
            //    cliente.Send(mensaje);
            //    cliente.
            //};


            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value));
        }

        public IActionResult Bloquear(string id)
        {
            if (id==null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Usuario.Bloquear(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _contenedorTrabajo.Usuario.Desbloquear(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

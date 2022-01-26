using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Utilidades
{
    public class EmailSender : IEmailSender
    {
        //Degradación del paquete detectada: Microsoft.Extensions.Identity.Stores de 3.1.13 a 3.1.0. Haga referencia al paquete directamente desde el proyecto para seleccionar una version diferente
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}

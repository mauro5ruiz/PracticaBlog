using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Repository
{
    public interface IUsuarioRepository : IRepository <ApplicationUser>
    {
        public void Bloquear(string idUsuario);
        public void Desbloquear(string id);
    }
}

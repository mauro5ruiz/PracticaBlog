using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Repository
{
    public interface IArticuloRepository : IRepository <Articulo>
    {
        void Update(Articulo articulo);
    }
}

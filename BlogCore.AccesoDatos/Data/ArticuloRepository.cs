using BlogCore.AccesoDatos.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext dbSet;

        public ArticuloRepository(ApplicationDbContext db) :base(db)
        {
            dbSet = db;
        }

        public void Update(Articulo articulo)
        {
            var objDesdeDb = dbSet.articulos.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.FechaCreacion = articulo.FechaCreacion;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.CategoriaId = articulo.CategoriaId;
            objDesdeDb.UrlImagen = articulo.UrlImagen;

            //dbSet.SaveChanges();
        }
    }
}

using BlogCore.AccesoDatos.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext dbSet;

        public CategoriaRepository(ApplicationDbContext db) :base(db)
        {
            dbSet = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return dbSet.categorias.Select(c => new SelectListItem()
            {
                Text = c.Nombre,
                Value = c.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = dbSet.categorias.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;

            dbSet.SaveChanges();
        }
    }
}

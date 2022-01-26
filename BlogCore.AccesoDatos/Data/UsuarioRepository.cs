using BlogCore.AccesoDatos.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext dbSet;

        public UsuarioRepository(ApplicationDbContext db) :base(db)
        {
            dbSet = db;
        }

        public void Bloquear(string idUsuario)
        {
            var usuarioDesdeBd = dbSet.applicationUsers.FirstOrDefault(u => u.Id == idUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(120);
            dbSet.SaveChanges();
        }

        public void Desbloquear(string id)
        {
            var usuarioDesdeBd = dbSet.applicationUsers.FirstOrDefault(u => u.Id == id);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;
            dbSet.SaveChanges();
        }
    }
}

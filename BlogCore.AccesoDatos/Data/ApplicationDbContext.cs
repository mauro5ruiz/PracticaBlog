﻿using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Articulo> articulos { get; set; }
        public DbSet<Slider> sliders { get; set; }

        public DbSet<ApplicationUser> applicationUsers { get; set; }
    }
}

using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext db;

        public SliderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            db = dbContext;
        }
        public void Update(Slider slider)
        {
            var objDesdeBd = db.sliders.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeBd.Nombre = slider.Nombre;
            objDesdeBd.Estado = slider.Estado;
            objDesdeBd.UrlImagen = slider.UrlImagen;
            db.SaveChanges();
        }
    }
}

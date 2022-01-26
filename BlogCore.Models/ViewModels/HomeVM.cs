using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Debe ingresar el nombre")]
        public string Nombre { get; set; }

        [Display(Name ="Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la ciudad")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Debe ingresar el país")]
        [Display(Name ="País")]
        public string Pais { get; set; }
    }
}

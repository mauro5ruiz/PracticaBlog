using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Debe ingresar el nombre de la categoría")]
        [Display(Name ="Categoría")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Orden de visualización")]
        public int Orden { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Debe ingresar el nombre del artículo")]
        [Display(Name ="Artículo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar la descripción del artículo")]
        public string Descripcion { get; set; }

        [Display(Name ="Fecha de creación")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Imagen")]
        public string UrlImagen { get; set; }

        [Required(ErrorMessage ="Debe seleccionar la categoría")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}

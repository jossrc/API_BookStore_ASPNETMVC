using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace API_BookStore_ASPNETMVC.Models
{
    public class Libro
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Isbn { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public int Paginas { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int IdAutor { get; set; }

        [Required]
        public int IdCategoria { get; set; }
    }
}
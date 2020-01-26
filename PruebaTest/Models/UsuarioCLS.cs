using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTest.Models
{
    public class UsuarioCLS
    {
        public int idUsuario { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Longitud maxima de 100 caracteres")]
        public string nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Longitud maxima de 100 caracteres")]
        public string apellido { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="Longitud maxima de 100 caracteres")]
        public string correo { get; set; }

        [Required(ErrorMessage ="Número de celular es requerido")]
        public long celular { get; set; }

        public int telefono { get; set; }

        public int habilitado { get; set; }
    }
}
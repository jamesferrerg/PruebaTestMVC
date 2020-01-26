using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTest.Models
{
    public class MensajeCLS
    {
        public int idMensaje { get; set; }

        [Required]
        [StringLength(250,ErrorMessage ="Cantidad de caracteres debe ser inferior a 250")]
        public string mensaje { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fechaPublicacion { get; set; }

        public int habilitado { get; set; }

        public int idUsuarioM { get; set; }

        public string nombreU { get; set; }
    }
}
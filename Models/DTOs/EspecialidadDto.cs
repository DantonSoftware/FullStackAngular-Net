using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class EspecialidadDto
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El nombre debe ser minimo 1 y maximo 60 carcatreres")]
        public string NombreEspecialidad { get; set; }
        [Required(ErrorMessage = "La descripcion es requerida")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe ser minimo 1 y maximo 100 carcatreres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage ="El estado es requerido")]
        public int Estado { get; set; }
    }
}

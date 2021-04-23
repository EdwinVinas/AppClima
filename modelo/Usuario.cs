using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Usuario
    {
        [Key]
        public string IdUsuario { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Apellido { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        public string UsuarioModifica { get; set; }
        public string ProgramaModifica { get; set; }
        public DateTime FechaModifica { get; set; }
        public string EquipoModifica { get; set; }

    }
}

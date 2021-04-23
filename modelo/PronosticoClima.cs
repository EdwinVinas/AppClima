using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class PronosticoClima
    {
        [Key]
        public int Cod_Div { get; set; }
        [Required, MaxLength(20)]
        public string Latitud { get; set; }
        [Required, MaxLength(20)]
        public string Longitud { get; set; }
        [Required, MaxLength(100)]
        public string Región { get; set; }
        [Required, MaxLength(100)]
        public string Departamento { get; set; }
        [Required, MaxLength(100)]
        public string Municipio { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public decimal Temperatura { get; set; }       
        public decimal Presion { get; set; }
        public decimal Precipitacion { get; set; }
        public decimal ProbabilidadTormenta { get; set; }
        public decimal Humedad { get; set; }
        public string Pronostico { get; set; }
        public string UsuarioModifica { get; set; }
        public string ProgramaModifica { get; set; }
        public DateTime FechaModifica { get; set; }
        public string EquipoModifica { get; set; }
    }
}

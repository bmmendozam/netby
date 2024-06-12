using System;
using System.ComponentModel.DataAnnotations;

namespace back
{


    public class TareaPendiente
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime Fecha_Vencimiento { get; set; }
        public bool Completada { get; set; }
    }
}

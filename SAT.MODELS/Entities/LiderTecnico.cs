using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class LiderTecnico
    {
        public int CodLiderTecnico { get; set; }
        public string CodUsuarioLider { get; set; }
        public int? CodTecnico { get; set; }
        public string CodUsuarioCad { get; set; }

        [ForeignKey("CodUsuarioLider")]
        public Usuario UsuarioLider { get; set; }
        [ForeignKey("CodTecnico")]
        public Tecnico Tecnico { get; set; }
    }
}

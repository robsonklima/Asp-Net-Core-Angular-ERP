using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnico
    {
        [Key]
        public int Id { get; set; }

        public int CodTecnico { get; set; }
        [ForeignKey("CodTecnico")]
        public Tecnico Tecnico { get; set; }

        public int CodOS { get; set; }
        [ForeignKey("CodOS")]
        public OrdemServico OS { get; set; }

        public string Title { get; set; }
        public string Color { get; set; }
        public byte? Visible { get; set; }

        public DateTime? DataHoraCad { get; set; }
    }
}

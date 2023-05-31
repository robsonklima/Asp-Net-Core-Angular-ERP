using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingIndiceDeReincidencia2
    {
        public int CodTipoEquip { get; set; }
        public int CodCliente { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodTecnico { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int ChamadosMes { get; set; }
        public int ChamadosMesReinc { get; set; }
        public int ChamadosAno { get; set; }
        public int ChamadosAnoReinc { get; set; }
        public int ChamadosMesReincMaquina { get; set; }
        public int ChamadosAnoReincMaquina { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DadosDashboardReincidenciaBackup")]
    public partial class DadosDashboardReincidenciaBackup
    {
        [StringLength(4)]
        public string Ano { get; set; }
        [StringLength(2)]
        public string Mes { get; set; }
        [StringLength(6)]
        public string AnoMes { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodCliente { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodTecnico { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesReinc { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingParqueResumoIten
    {
        public int? AnoMes { get; set; }
        public int? CodCliente { get; set; }
        public int? CodContrato { get; set; }
        public int? CodEquip { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(50)]
        public string Autorizada { get; set; }
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(5)]
        public string Agencia { get; set; }
        [Column("DC")]
        [StringLength(4)]
        public string Dc { get; set; }
        [Column("_Local")]
        [StringLength(50)]
        public string Local { get; set; }
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [StringLength(20)]
        public string Serie { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
        [Column("QtdOSMaquina")]
        public int? QtdOsmaquina { get; set; }
        [Column("QtdOSExtraMaquina")]
        public int? QtdOsextraMaquina { get; set; }
    }
}

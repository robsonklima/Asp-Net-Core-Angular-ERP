using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcMtbf11
    {
        public int CodEquipContrato { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodRegiao { get; set; }
        public int? QtdDiaInstalado { get; set; }
        [Column("QtdOSMaquina")]
        public int? QtdOsmaquina { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
        [Column("QtdOSGeralCorretivas")]
        public int? QtdOsgeralCorretivas { get; set; }
        [Column("QtdOSGeralPreventiva")]
        public int? QtdOsgeralPreventiva { get; set; }
        public int? QtdDiaInstaladoR1 { get; set; }
        [Column("QtdOSMaquinaR1")]
        public int? QtdOsmaquinaR1 { get; set; }
        [Column("QtdOSGeralR1")]
        public int? QtdOsgeralR1 { get; set; }
        [Column("QtdOSGeralCorretivasR1")]
        public int? QtdOsgeralCorretivasR1 { get; set; }
        public int? QtdDiaInstaladoR2 { get; set; }
        [Column("QtdOSMaquinaR2")]
        public int? QtdOsmaquinaR2 { get; set; }
        [Column("QtdOSGeralR2")]
        public int? QtdOsgeralR2 { get; set; }
        [Column("QtdOSGeralCorretivasR2")]
        public int? QtdOsgeralCorretivasR2 { get; set; }
        public int? QtdDiaInstaladoR3 { get; set; }
        [Column("QtdOSMaquinaR3")]
        public int? QtdOsmaquinaR3 { get; set; }
        [Column("QtdOSGeralR3")]
        public int? QtdOsgeralR3 { get; set; }
        [Column("QtdOSGeralCorretivasR3")]
        public int? QtdOsgeralCorretivasR3 { get; set; }
        public int? QtdDiaInstalado30 { get; set; }
        [Column("QtdOSMaquina30")]
        public int? QtdOsmaquina30 { get; set; }
        [Column("QtdOSGeral30")]
        public int? QtdOsgeral30 { get; set; }
        [Column("QtdOSGeralCorretivas30")]
        public int? QtdOsgeralCorretivas30 { get; set; }
        public int? QtdDiaInstalado60 { get; set; }
        [Column("QtdOSMaquina60")]
        public int? QtdOsmaquina60 { get; set; }
        [Column("QtdOSGeral60")]
        public int? QtdOsgeral60 { get; set; }
        [Column("QtdOSGeralCorretivas60")]
        public int? QtdOsgeralCorretivas60 { get; set; }
        public int? QtdDiaInstalado90 { get; set; }
        [Column("QtdOSMaquina90")]
        public int? QtdOsmaquina90 { get; set; }
        [Column("QtdOSGeral90")]
        public int? QtdOsgeral90 { get; set; }
        [Column("QtdOSGeralCorretivas90")]
        public int? QtdOsgeralCorretivas90 { get; set; }
    }
}

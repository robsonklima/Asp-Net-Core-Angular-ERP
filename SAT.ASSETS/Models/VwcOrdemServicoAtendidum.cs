﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcOrdemServicoAtendidum
    {
        [Column("CodOSAtendidas")]
        public int CodOsatendidas { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column("DataHoraFechamentoOS", TypeName = "datetime")]
        public DateTime? DataHoraFechamentoOs { get; set; }
        public int? CodCliente { get; set; }
        public int? CodFilial { get; set; }
        public int? AnoMes { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodPosto { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        [Required]
        [StringLength(3)]
        public string IndServico { get; set; }
    }
}

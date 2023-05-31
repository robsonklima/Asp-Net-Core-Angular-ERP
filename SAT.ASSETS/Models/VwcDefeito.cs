﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDefeito
    {
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodRATDetalhe")]
        public int CodRatdetalhe { get; set; }
        public int CodTipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        public int CodTipoCausa { get; set; }
        public int CodGrupoCausa { get; set; }
        public int CodCausa { get; set; }
        public int CodDefeito { get; set; }
        public int? CodPeca { get; set; }
        public int CodAcao { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        [Required]
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        public int CodEquipContrato { get; set; }
        public byte? IndGarantia { get; set; }
    }
}
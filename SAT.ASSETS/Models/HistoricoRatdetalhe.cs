using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("HistoricoRATDetalhes")]
    public partial class HistoricoRatdetalhe
    {
        [Column("CodHistoricoRATDetalhe")]
        public int CodHistoricoRatdetalhe { get; set; }
        [Column("CodRATDetalhe")]
        public int CodRatdetalhe { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        public int? CodServico { get; set; }
        public int CodTipoCausa { get; set; }
        public int CodGrupoCausa { get; set; }
        public int CodDefeito { get; set; }
        public int CodCausa { get; set; }
        public int CodAcao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? CodOrigemCausa { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(20)]
        public string CodUsuarioExclusao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraExclusao { get; set; }
    }
}

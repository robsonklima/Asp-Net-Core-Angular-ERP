using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATDetalhes_ARQ_MORTO")]
    public partial class RatdetalhesArqMorto
    {
        [Key]
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
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public byte? IndDefeitoMaquina { get; set; }
    }
}

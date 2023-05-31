using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DespesaTentativaKM_ARQ_MORTO")]
    public partial class DespesaTentativaKmArqMorto
    {
        [Column("CodDespesaTentativaKM")]
        public int CodDespesaTentativaKm { get; set; }
        public int? CodDespesa { get; set; }
        public int? CodRat { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodFilial { get; set; }
        public int? CodDespesaTipo { get; set; }
        [Column("TentativaKM")]
        [StringLength(200)]
        public string TentativaKm { get; set; }
        [StringLength(200)]
        public string EnderecoOrigem { get; set; }
        [StringLength(50)]
        public string NumOrigem { get; set; }
        [StringLength(100)]
        public string BairroOrigem { get; set; }
        public int? CodCidadeOrigem { get; set; }
        [StringLength(200)]
        public string EnderecoDestino { get; set; }
        [StringLength(50)]
        public string NumDestino { get; set; }
        [StringLength(100)]
        public string BairroDestino { get; set; }
        public int? CodCidadeDestino { get; set; }
        public byte IndVisualizado { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? Sequencia { get; set; }
    }
}

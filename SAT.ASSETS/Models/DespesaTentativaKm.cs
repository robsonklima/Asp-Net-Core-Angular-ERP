using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaTentativaKM")]
    public partial class DespesaTentativaKm
    {
        [Key]
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

        [ForeignKey(nameof(CodCidadeDestino))]
        [InverseProperty(nameof(Cidade.DespesaTentativaKmCodCidadeDestinoNavigations))]
        public virtual Cidade CodCidadeDestinoNavigation { get; set; }
        [ForeignKey(nameof(CodCidadeOrigem))]
        [InverseProperty(nameof(Cidade.DespesaTentativaKmCodCidadeOrigemNavigations))]
        public virtual Cidade CodCidadeOrigemNavigation { get; set; }
        [ForeignKey(nameof(CodDespesa))]
        [InverseProperty(nameof(Despesa.DespesaTentativaKms))]
        public virtual Despesa CodDespesaNavigation { get; set; }
        [ForeignKey(nameof(CodDespesaTipo))]
        [InverseProperty(nameof(DespesaTipo.DespesaTentativaKms))]
        public virtual DespesaTipo CodDespesaTipoNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.DespesaTentativaKms))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodRat))]
        [InverseProperty(nameof(Rat.DespesaTentativaKms))]
        public virtual Rat CodRatNavigation { get; set; }
        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.DespesaTentativaKms))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
    }
}

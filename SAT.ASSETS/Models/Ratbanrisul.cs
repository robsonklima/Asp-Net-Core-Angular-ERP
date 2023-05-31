using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATBanrisul")]
    public partial class Ratbanrisul
    {
        [Key]
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column("CodRATBanrisul")]
        public int CodRatbanrisul { get; set; }
        [StringLength(20)]
        public string NumSerieInst { get; set; }
        [StringLength(20)]
        public string NumSerieRet { get; set; }
        [StringLength(70)]
        public string Rede { get; set; }
        public int? CodTipoComunicacao { get; set; }
        [StringLength(50)]
        public string NumeroChipInstalado { get; set; }
        public int? CodOperadoraTelefoniaChipInstalado { get; set; }
        [StringLength(50)]
        public string NumeroChipRetirado { get; set; }
        public int? CodOperadoraTelefoniaChipRetirado { get; set; }
        public int? CodMotivoComunicacao { get; set; }
        [StringLength(2000)]
        public string ObsMotivoComunicacao { get; set; }
        public bool? AtendimentoRealizadoPorTelefone { get; set; }
        public int? CodEquipRet { get; set; }
        public int? CodEquipInst { get; set; }
        public byte? IndSmartphone { get; set; }
        [Column("CodDefeitoPOS")]
        public int? CodDefeitoPos { get; set; }
        public int? CodMotivoCancelamento { get; set; }
        [StringLength(2000)]
        public string ObsMotivoCancelamento { get; set; }

        [ForeignKey(nameof(CodMotivoComunicacao))]
        [InverseProperty(nameof(MotivoComunicacao.Ratbanrisuls))]
        public virtual MotivoComunicacao CodMotivoComunicacaoNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefoniaChipInstalado))]
        [InverseProperty(nameof(OperadoraTelefonium.RatbanrisulCodOperadoraTelefoniaChipInstaladoNavigations))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaChipInstaladoNavigation { get; set; }
        [ForeignKey(nameof(CodOperadoraTelefoniaChipRetirado))]
        [InverseProperty(nameof(OperadoraTelefonium.RatbanrisulCodOperadoraTelefoniaChipRetiradoNavigations))]
        public virtual OperadoraTelefonium CodOperadoraTelefoniaChipRetiradoNavigation { get; set; }
        [ForeignKey(nameof(CodRat))]
        [InverseProperty(nameof(Rat.Ratbanrisul))]
        public virtual Rat CodRatNavigation { get; set; }
        [ForeignKey(nameof(CodTipoComunicacao))]
        [InverseProperty(nameof(TipoComunicacao.Ratbanrisuls))]
        public virtual TipoComunicacao CodTipoComunicacaoNavigation { get; set; }
    }
}

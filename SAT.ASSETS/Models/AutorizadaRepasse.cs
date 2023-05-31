using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AutorizadaRepasse")]
    public partial class AutorizadaRepasse
    {
        [Key]
        public int CodAutorizadaRepasse { get; set; }
        public int? CodContrato { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodCliente { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorRepasseMensal { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorRepasseInstalacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodAutorizada))]
        [InverseProperty(nameof(Autorizadum.AutorizadaRepasses))]
        public virtual Autorizadum CodAutorizadaNavigation { get; set; }
        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.AutorizadaRepasses))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.AutorizadaRepasses))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodFilial))]
        [InverseProperty(nameof(Filial.AutorizadaRepasses))]
        public virtual Filial CodFilialNavigation { get; set; }
        [ForeignKey(nameof(CodRegiao))]
        [InverseProperty(nameof(Regiao.AutorizadaRepasses))]
        public virtual Regiao CodRegiaoNavigation { get; set; }
        [ForeignKey(nameof(CodSla))]
        [InverseProperty(nameof(Sla.AutorizadaRepasses))]
        public virtual Sla CodSlaNavigation { get; set; }
    }
}

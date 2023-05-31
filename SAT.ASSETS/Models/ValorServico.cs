using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ValorServico")]
    [Index(nameof(CodContrato), nameof(CodSla), nameof(CodAutorizada), nameof(CodFilial), nameof(CodTipoEquip), nameof(CodGrupoEquip), nameof(CodEquip), nameof(CodServico), nameof(CodRegiao), nameof(CodCliente), Name = "IX_ValorServico", IsUnique = true)]
    public partial class ValorServico
    {
        [Key]
        public int CodValorServico { get; set; }
        public int? CodContrato { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public int? CodServico { get; set; }
        [Column("ValorServico", TypeName = "money")]
        public decimal? ValorServico1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorInstalacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManutencao { get; set; }
        public int? CodRegiao { get; set; }
        [Column("indAtivo")]
        public byte? IndAtivo { get; set; }
        public int? CodCliente { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.ValorServicos))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.ValorServicos))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodRegiao))]
        [InverseProperty(nameof(Regiao.ValorServicos))]
        public virtual Regiao CodRegiaoNavigation { get; set; }
        [ForeignKey(nameof(CodServico))]
        [InverseProperty(nameof(TipoServico.ValorServicos))]
        public virtual TipoServico CodServicoNavigation { get; set; }
        [ForeignKey(nameof(CodSla))]
        [InverseProperty(nameof(Sla.ValorServicos))]
        public virtual Sla CodSlaNavigation { get; set; }
    }
}

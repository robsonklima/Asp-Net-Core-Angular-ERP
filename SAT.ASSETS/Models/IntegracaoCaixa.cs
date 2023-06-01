using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoCaixa")]
    public partial class IntegracaoCaixa
    {
        public IntegracaoCaixa()
        {
            IntegracaoCaixaAlteracaoStatuses = new HashSet<IntegracaoCaixaAlteracaoStatus>();
            IntegracaoCaixaArquivos = new HashSet<IntegracaoCaixaArquivo>();
        }

        [Key]
        public int CodIntegracaoCaixa { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public string Req { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAbertura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAceite { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraRecusa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraConclusao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCancelamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.IntegracaoCaixas))]
        public virtual O CodOsNavigation { get; set; }
        [InverseProperty(nameof(IntegracaoCaixaAlteracaoStatus.CodIntegracaoCaixaNavigation))]
        public virtual ICollection<IntegracaoCaixaAlteracaoStatus> IntegracaoCaixaAlteracaoStatuses { get; set; }
        [InverseProperty(nameof(IntegracaoCaixaArquivo.CodIntegracaoCaixaNavigation))]
        public virtual ICollection<IntegracaoCaixaArquivo> IntegracaoCaixaArquivos { get; set; }
    }
}

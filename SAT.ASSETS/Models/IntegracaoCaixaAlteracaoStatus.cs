using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoCaixaAlteracaoStatus")]
    public partial class IntegracaoCaixaAlteracaoStatus
    {
        [Key]
        public int CodIntegracaoCaixaAlteracaoStatus { get; set; }
        public int CodIntegracaoCaixa { get; set; }
        public int CodStatusServico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraNotificacaoAlteracaoStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodIntegracaoCaixa))]
        [InverseProperty(nameof(IntegracaoCaixa.IntegracaoCaixaAlteracaoStatuses))]
        public virtual IntegracaoCaixa CodIntegracaoCaixaNavigation { get; set; }
    }
}

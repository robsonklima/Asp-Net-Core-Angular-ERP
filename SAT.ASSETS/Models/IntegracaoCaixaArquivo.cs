using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoCaixaArquivo")]
    public partial class IntegracaoCaixaArquivo
    {
        [Key]
        public int CodIntegracaoCaixaArquivo { get; set; }
        public int CodIntegracaoCaixa { get; set; }
        public int CodIntegracaoCaixaTipoArquivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [Column(TypeName = "xml")]
        public string Conteudo { get; set; }

        [ForeignKey(nameof(CodIntegracaoCaixa))]
        [InverseProperty(nameof(IntegracaoCaixa.IntegracaoCaixaArquivos))]
        public virtual IntegracaoCaixa CodIntegracaoCaixaNavigation { get; set; }
        [ForeignKey(nameof(CodIntegracaoCaixaTipoArquivo))]
        [InverseProperty(nameof(IntegracaoCaixaTipoArquivo.IntegracaoCaixaArquivos))]
        public virtual IntegracaoCaixaTipoArquivo CodIntegracaoCaixaTipoArquivoNavigation { get; set; }
    }
}

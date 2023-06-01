using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoCaixaTipoArquivo")]
    public partial class IntegracaoCaixaTipoArquivo
    {
        public IntegracaoCaixaTipoArquivo()
        {
            IntegracaoCaixaArquivos = new HashSet<IntegracaoCaixaArquivo>();
        }

        [Key]
        public int CodIntegracaoCaixaTipoArquivo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [InverseProperty(nameof(IntegracaoCaixaArquivo.CodIntegracaoCaixaTipoArquivoNavigation))]
        public virtual ICollection<IntegracaoCaixaArquivo> IntegracaoCaixaArquivos { get; set; }
    }
}

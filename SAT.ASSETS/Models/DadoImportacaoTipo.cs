using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DadoImportacaoTipo")]
    public partial class DadoImportacaoTipo
    {
        public DadoImportacaoTipo()
        {
            DadoImportacaos = new HashSet<DadoImportacao>();
        }

        [Key]
        public int CodDadoImportacaoTipo { get; set; }
        [StringLength(50)]
        public string NomeDadoImportacaoTipo { get; set; }
        public int IndAtivo { get; set; }

        [InverseProperty(nameof(DadoImportacao.CodDadoImportacaoTipoNavigation))]
        public virtual ICollection<DadoImportacao> DadoImportacaos { get; set; }
    }
}

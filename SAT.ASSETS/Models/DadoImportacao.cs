using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DadoImportacao")]
    public partial class DadoImportacao
    {
        [Key]
        public int CodDadoImportacao { get; set; }
        [Required]
        [StringLength(200)]
        public string CaminhoArquivo { get; set; }
        public int CodDadoImportacaoTipo { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(50)]
        public string NomePecaLista { get; set; }
        public int IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodDadoImportacaoTipo))]
        [InverseProperty(nameof(DadoImportacaoTipo.DadoImportacaos))]
        public virtual DadoImportacaoTipo CodDadoImportacaoTipoNavigation { get; set; }
    }
}

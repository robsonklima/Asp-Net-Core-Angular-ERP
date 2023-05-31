using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class ArquivosChamado
    {
        [Key]
        public int CodArquivo { get; set; }
        [Key]
        public int CodArquivoAuto { get; set; }
        public int? CodCliente { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraRecebimento { get; set; }
        [Column(TypeName = "text")]
        public string Conteudo { get; set; }
        public byte? IndSucesso { get; set; }
        [StringLength(100)]
        public string NomeArquivo { get; set; }
        public byte? CodProcedencia { get; set; }
        [StringLength(100)]
        public string ServicoEmail { get; set; }
    }
}

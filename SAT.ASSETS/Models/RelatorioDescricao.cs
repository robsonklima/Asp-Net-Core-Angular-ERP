using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RelatorioDescricao")]
    public partial class RelatorioDescricao
    {
        [Key]
        public int CodRelatorioDescricao { get; set; }
        public string Nome { get; set; }
        public string Projeto { get; set; }
        public string Objetivo { get; set; }
        public string Responsavel { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataLiberacao { get; set; }
        public string Desenvolvedor { get; set; }
        public string Definicoes { get; set; }
    }
}

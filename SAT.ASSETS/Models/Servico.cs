using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Servico")]
    public partial class Servico
    {
        [Key]
        public int CodServico { get; set; }
        [StringLength(50)]
        public string NomeServico { get; set; }
        public byte IndEmProcesso { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataUltimoProcessamento { get; set; }
        public byte IndAtivo { get; set; }
    }
}

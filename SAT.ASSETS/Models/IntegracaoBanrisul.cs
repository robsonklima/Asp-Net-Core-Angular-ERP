using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoBanrisul")]
    public partial class IntegracaoBanrisul
    {
        [Key]
        public int CodArquivoBanrisul { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeArquivo { get; set; }
        public int Sequencial { get; set; }
    }
}

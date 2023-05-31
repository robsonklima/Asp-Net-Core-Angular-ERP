using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ModeloArquivo")]
    public partial class ModeloArquivo
    {
        [Key]
        [Column(TypeName = "numeric(18, 0)")]
        public decimal CodModeloArquivo { get; set; }
        [Required]
        [StringLength(60)]
        public string DescModeloArquivo { get; set; }
        [StringLength(60)]
        public string NomeModeloArquivo { get; set; }
        [StringLength(60)]
        public string NomeManual { get; set; }
        public byte IndAtivo { get; set; }
    }
}

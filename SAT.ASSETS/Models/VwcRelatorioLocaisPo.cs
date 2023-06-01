using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelatorioLocaisPo
    {
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
        [StringLength(20)]
        public string Serie { get; set; }
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(150)]
        public string Endereco { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [StringLength(50)]
        public string Fone { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAtivacao { get; set; }
        [Required]
        [Column("TIPO")]
        [StringLength(39)]
        public string Tipo { get; set; }
        [Required]
        [StringLength(3)]
        public string RemanejamentoComSucesso { get; set; }
        [Required]
        [StringLength(3)]
        public string RemanejamentoCancelado { get; set; }
    }
}

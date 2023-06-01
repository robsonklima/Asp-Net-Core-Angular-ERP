using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTarefa
    {
        [Column("CÓDIGO")]
        public int Código { get; set; }
        [Column("ABERTURA", TypeName = "datetime")]
        public DateTime Abertura { get; set; }
        [Required]
        [Column("SOLICITANTE")]
        [StringLength(50)]
        public string Solicitante { get; set; }
        [Required]
        [Column("TÍTULO")]
        public string Título { get; set; }
        [Column("DESCRIÇÃO")]
        public string Descrição { get; set; }
        [Column("FECHAMENTO", TypeName = "datetime")]
        public DateTime? Fechamento { get; set; }
        [Column("SOLUÇÃO")]
        public string Solução { get; set; }
    }
}

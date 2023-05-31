using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentoPecaExportacao")]
    public partial class OrcamentoPecaExportacao
    {
        [Key]
        public int CodOrcamentoPecaExportacao { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroOrcamento { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [StringLength(100)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(100)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public string CaminhoArquivoOrcamento { get; set; }
        public int CodStatusOrcamento { get; set; }
    }
}

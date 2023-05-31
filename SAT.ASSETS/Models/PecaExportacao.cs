using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PecaExportacao")]
    public partial class PecaExportacao
    {
        [Key]
        public int CodPecaExportacao { get; set; }
        [Required]
        [StringLength(50)]
        public string CodPai { get; set; }
        [Required]
        [StringLength(50)]
        public string CodPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValPecaDolar { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(200)]
        public string NomePeca { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalNFAut")]
    public partial class InstalNfaut
    {
        [Key]
        [Column("CodInstalNFAut")]
        public int CodInstalNfaut { get; set; }
        public int CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [Column("NFAut")]
        [StringLength(20)]
        public string Nfaut { get; set; }
        [Column("DataNFAut", TypeName = "datetime")]
        public DateTime DataNfaut { get; set; }
        [Column("VlrNFAut", TypeName = "decimal(10, 2)")]
        public decimal VlrNfaut { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}

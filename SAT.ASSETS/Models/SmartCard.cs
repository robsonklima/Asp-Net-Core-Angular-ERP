using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SmartCard")]
    public partial class SmartCard
    {
        [Key]
        public int CodSmartCard { get; set; }
        [Required]
        [StringLength(16)]
        public string SerialNumber { get; set; }
        [Required]
        [Column("IC")]
        [StringLength(8)]
        public string Ic { get; set; }
        [Required]
        [StringLength(8)]
        public string CardKey { get; set; }
        [Required]
        [StringLength(8)]
        public string TerminalKey { get; set; }
    }
}

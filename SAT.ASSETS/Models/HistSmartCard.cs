using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistSmartCard")]
    public partial class HistSmartCard
    {
        [Key]
        public int CodHistorico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraHist { get; set; }
        public int CodSmartCard { get; set; }
        [StringLength(16)]
        public string SerialNumber { get; set; }
        [Column("IC")]
        [StringLength(8)]
        public string Ic { get; set; }
        [StringLength(8)]
        public string CardKey { get; set; }
        [StringLength(8)]
        public string TerminalKey { get; set; }
    }
}

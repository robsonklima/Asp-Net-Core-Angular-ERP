using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("InstalNFVenda")]
    public partial class InstalNfvendum
    {
        [Key]
        [Column("CodInstalNFVenda")]
        public int CodInstalNfvenda { get; set; }
        public int CodCliente { get; set; }
        [Column("NumNFVenda")]
        public int NumNfvenda { get; set; }
        [Column("DataNFVenda", TypeName = "datetime")]
        public DateTime? DataNfvenda { get; set; }
        [Column("ObsNFVenda")]
        [StringLength(50)]
        public string ObsNfvenda { get; set; }
        [Column("DataNFVendaEnvioCliente", TypeName = "datetime")]
        public DateTime? DataNfvendaEnvioCliente { get; set; }
        [Column("DataNFVendaRecebimentoCliente", TypeName = "datetime")]
        public DateTime? DataNfvendaRecebimentoCliente { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}

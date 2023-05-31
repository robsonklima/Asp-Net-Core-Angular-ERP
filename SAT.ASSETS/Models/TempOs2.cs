using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TempOS2")]
    public partial class TempOs2
    {
        [Column("CHAMADOCOBRA")]
        [StringLength(255)]
        public string Chamadocobra { get; set; }
        [Column("NUMEROOS")]
        [StringLength(255)]
        public string Numeroos { get; set; }
        [Column("NROCHAMADO")]
        [StringLength(255)]
        public string Nrochamado { get; set; }
        [Column("TPA")]
        [StringLength(255)]
        public string Tpa { get; set; }
        [Column("AGENCIA")]
        [StringLength(255)]
        public string Agencia { get; set; }
        [Column("SAG")]
        [StringLength(255)]
        public string Sag { get; set; }
        [Column("DEPENDENCIA")]
        [StringLength(255)]
        public string Dependencia { get; set; }
        [Column("CIDADE")]
        [StringLength(255)]
        public string Cidade { get; set; }
        [Column("UF")]
        [StringLength(255)]
        public string Uf { get; set; }
        [Column("SEMAT")]
        [StringLength(255)]
        public string Semat { get; set; }
        [Column("CRITICIDADE")]
        public double? Criticidade { get; set; }
        [Column("NROCONTRATO")]
        [StringLength(255)]
        public string Nrocontrato { get; set; }
        [Column("NUMEROBEM")]
        [StringLength(255)]
        public string Numerobem { get; set; }
        [Column("DTACHAMADO", TypeName = "datetime")]
        public DateTime? Dtachamado { get; set; }
        [Column("DTALIMITE", TypeName = "datetime")]
        public DateTime? Dtalimite { get; set; }
        [Column("STATUS")]
        [StringLength(255)]
        public string Status { get; set; }
        [Column("MANTENED")]
        [StringLength(255)]
        public string Mantened { get; set; }
    }
}

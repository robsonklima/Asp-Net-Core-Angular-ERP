using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TMPBB")]
    public partial class Tmpbb
    {
        [Column("OS")]
        [StringLength(255)]
        public string Os { get; set; }
        [Column("UNIV")]
        [StringLength(255)]
        public string Univ { get; set; }
        [Column("SIT")]
        [StringLength(255)]
        public string Sit { get; set; }
        [Column("DATA AB", TypeName = "datetime")]
        public DateTime? DataAb { get; set; }
        [Column("UF")]
        [StringLength(255)]
        public string Uf { get; set; }
        [Column("DESCRIÇÃO")]
        [StringLength(255)]
        public string Descrição { get; set; }
        [Column("dias")]
        public double? Dias { get; set; }
        [Column("responsavel")]
        [StringLength(255)]
        public string Responsavel { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TabelaErrosHitachi")]
    public partial class TabelaErrosHitachi
    {
        [Column("Error code")]
        [StringLength(255)]
        public string ErrorCode { get; set; }
        [Column("Error Name")]
        [StringLength(255)]
        public string ErrorName { get; set; }
        public string Causes { get; set; }
        [Column("Order of Priority")]
        [StringLength(255)]
        public string OrderOfPriority { get; set; }
        [Column("Check Point")]
        [StringLength(255)]
        public string CheckPoint { get; set; }
    }
}

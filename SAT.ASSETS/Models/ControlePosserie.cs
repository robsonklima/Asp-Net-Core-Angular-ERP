using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ControlePOSSerie")]
    public partial class ControlePosserie
    {
        [Column("CodControlePOSSerie")]
        public int CodControlePosserie { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

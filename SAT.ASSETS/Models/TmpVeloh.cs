using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TmpVeloh")]
    public partial class TmpVeloh
    {
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

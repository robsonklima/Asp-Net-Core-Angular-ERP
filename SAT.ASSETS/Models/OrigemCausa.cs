using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrigemCausa")]
    public partial class OrigemCausa
    {
        [Key]
        public int CodOrigemCausa { get; set; }
        [StringLength(50)]
        public string DescOrigemCausa { get; set; }
        [StringLength(20)]
        public string AbrevOrigemCausa { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
    }
}

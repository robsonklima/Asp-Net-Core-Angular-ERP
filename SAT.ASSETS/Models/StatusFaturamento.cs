using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusFaturamento")]
    public partial class StatusFaturamento
    {
        [Key]
        public int CodStatusFaturamento { get; set; }
        [Required]
        [StringLength(50)]
        public string DescStatusFaturamento { get; set; }
        public int IndAtivo { get; set; }
        [StringLength(50)]
        public string DescEnum { get; set; }
    }
}

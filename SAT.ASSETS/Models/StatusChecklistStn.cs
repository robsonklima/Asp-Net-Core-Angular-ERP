using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusChecklistSTN")]
    public partial class StatusChecklistStn
    {
        [Key]
        public int CodStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string DescStatus { get; set; }
        public int IndAtivo { get; set; }
    }
}

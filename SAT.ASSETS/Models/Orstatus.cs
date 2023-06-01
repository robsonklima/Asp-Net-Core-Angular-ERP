using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ORStatus")]
    public partial class Orstatus
    {
        [Key]
        public int CodStatus { get; set; }
        [Required]
        [StringLength(100)]
        public string DescStatus { get; set; }
        public int? IndAtivo { get; set; }
    }
}

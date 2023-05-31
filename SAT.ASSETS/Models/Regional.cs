using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Regional")]
    public partial class Regional
    {
        [Key]
        public int CodRegional { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeRegional { get; set; }
        [Required]
        [StringLength(30)]
        public string Email { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

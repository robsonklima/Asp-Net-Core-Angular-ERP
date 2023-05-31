using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoFalha")]
    public partial class IntegracaoFalha
    {
        [Key]
        public int CodIntegracaoFalha { get; set; }
        public int CodIntegracaoEmail { get; set; }
        [Required]
        public string DetalheFalha { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraFalha { get; set; }
    }
}

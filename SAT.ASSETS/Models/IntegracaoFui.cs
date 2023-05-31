using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("IntegracaoFui")]
    public partial class IntegracaoFui
    {
        [Key]
        public int CodIntegracaoFui { get; set; }
        public int? CodOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public int? Status { get; set; }
    }
}

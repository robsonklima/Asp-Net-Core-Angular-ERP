using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcStatus")]
    public partial class OrcStatus
    {
        [Key]
        public int CodOrcStatus { get; set; }
        [StringLength(255)]
        public string Nome { get; set; }
    }
}

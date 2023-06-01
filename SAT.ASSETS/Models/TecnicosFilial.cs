using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TecnicosFilial")]
    public partial class TecnicosFilial
    {
        [Key]
        public int CodTecnicosFilial { get; set; }
        public int? CodFilial { get; set; }
        public int? QtdTecnicos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
    }
}

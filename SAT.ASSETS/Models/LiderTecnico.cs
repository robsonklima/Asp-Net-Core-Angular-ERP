using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LiderTecnico")]
    public partial class LiderTecnico
    {
        [Key]
        public int CodLiderTecnico { get; set; }
        [StringLength(60)]
        public string CodUsuarioLider { get; set; }
        public int? CodTecnico { get; set; }
        [StringLength(60)]
        public string CodUsuarioCad { get; set; }
    }
}

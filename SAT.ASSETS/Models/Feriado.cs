using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class Feriado
    {
        [Key]
        public int CodFeriado { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFeriado { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Data { get; set; }
        public short? QtdeDias { get; set; }
        public int? CodPais { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int? CodCidade { get; set; }
    }
}

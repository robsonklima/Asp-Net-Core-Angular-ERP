using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CedulasMecanismoRAT")]
    public partial class CedulasMecanismoRat
    {
        [Key]
        public int CodRatMecanismo { get; set; }
        public int CodRat { get; set; }
        public int? CedulasPagas { get; set; }
        public int? CedulasRejeitadas { get; set; }
        public string Justificativa { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
    }
}

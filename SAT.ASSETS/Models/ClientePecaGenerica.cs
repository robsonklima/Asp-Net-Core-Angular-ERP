﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ClientePecaGenerica")]
    public partial class ClientePecaGenerica
    {
        [Key]
        public int CodClientePecaGenerica { get; set; }
        public int CodPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorUnitario { get; set; }
        [Column("ValorIPI", TypeName = "decimal(10, 2)")]
        public decimal? ValorIpi { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrSubstituicaoNovo { get; set; }
        [Column("vlrBaseTroca", TypeName = "decimal(10, 2)")]
        public decimal? VlrBaseTroca { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
    }
}

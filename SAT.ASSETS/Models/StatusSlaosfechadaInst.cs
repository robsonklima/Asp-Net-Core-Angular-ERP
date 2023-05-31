﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("StatusSLAOSFechadaInst")]
    public partial class StatusSlaosfechadaInst
    {
        [Column("CodStatusSLAOSFechadaInst")]
        public int CodStatusSlaosfechadaInst { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [Column("StatusSLAOS")]
        [StringLength(15)]
        public string StatusSlaos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraFechamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraLimiteAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraProcessamento { get; set; }
        [Column("KMDistancia")]
        public int Kmdistancia { get; set; }
    }
}
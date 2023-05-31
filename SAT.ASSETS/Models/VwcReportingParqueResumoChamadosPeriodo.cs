﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingParqueResumoChamadosPeriodo
    {
        public int? AnoMes { get; set; }
        public int? CodEquip { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        public int? CodCliente { get; set; }
        [Column("QtdOSGeral")]
        public int? QtdOsgeral { get; set; }
        public int? QtdGeral { get; set; }
        public int? QtdEquipGeral { get; set; }
        public int? QtdMaquina { get; set; }
        public int? QtdEquipMaquina { get; set; }
        public int? QtdExtraMaquina { get; set; }
        public int? QtdEquipExtraMaquina { get; set; }
        public int QtdInc { get; set; }
        public int QtdIncMaquina { get; set; }
        public int QtdIncExtraMaquina { get; set; }
    }
}

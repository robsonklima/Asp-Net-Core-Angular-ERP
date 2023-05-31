﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingIndiceDeQuebra
    {
        public int CodTipoEquip { get; set; }
        public int CodCliente { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        public int CodRegiao { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int? ParqueAtivo { get; set; }
        public int ChamadosAbertos { get; set; }
        public int ChamadosMes { get; set; }
        public int ChamadosAno { get; set; }
    }
}
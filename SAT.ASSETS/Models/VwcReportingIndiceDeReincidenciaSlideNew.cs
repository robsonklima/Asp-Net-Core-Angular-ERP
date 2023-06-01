﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingIndiceDeReincidenciaSlideNew
    {
        public int CodTipoEquip { get; set; }
        public int CodCliente { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int? CodTecnico { get; set; }
        public int? ChamadosMes { get; set; }
        public int? ChamadosMesReinc { get; set; }
    }
}

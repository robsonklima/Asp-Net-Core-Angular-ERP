﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ReportingIndiceDePecasPendentesGeovane
    {
        public int CodTipoEquip { get; set; }
        public int CodCliente { get; set; }
        public int CodFilial { get; set; }
        public int CodAutorizada { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int ChamadosMes { get; set; }
        public int ChamadosMesPecasPendentes { get; set; }
    }
}

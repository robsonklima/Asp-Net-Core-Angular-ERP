﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcContratoSla
    {
        public int CodContrato { get; set; }
        [StringLength(20)]
        public string NroContrato { get; set; }
        [Column("CodSLA")]
        public int CodSla { get; set; }
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
    }
}
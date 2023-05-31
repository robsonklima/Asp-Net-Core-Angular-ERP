﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTmpMonitoramento27
    {
        [Column("descricao")]
        [StringLength(1000)]
        public string Descricao { get; set; }
    }
}

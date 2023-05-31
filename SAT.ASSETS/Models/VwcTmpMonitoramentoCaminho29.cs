using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcTmpMonitoramentoCaminho29
    {
        [StringLength(97)]
        public string Caminho { get; set; }
    }
}

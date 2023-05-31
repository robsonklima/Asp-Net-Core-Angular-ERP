using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SlideContrato_12Meses")]
    public partial class SlideContrato12mese
    {
        [StringLength(6)]
        public string AnoMes { get; set; }
        public int? Vigentes { get; set; }
        public int? Renovados { get; set; }
        public int? Reajustados { get; set; }
        public int? NaoReajustados { get; set; }
        public int? EmNegociacao { get; set; }
        public int? Declinados { get; set; }
    }
}

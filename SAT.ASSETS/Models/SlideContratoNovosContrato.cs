using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SlideContrato_NovosContratos")]
    public partial class SlideContratoNovosContrato
    {
        [StringLength(6)]
        public string AnoMes { get; set; }
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(50)]
        public string NroContrato { get; set; }
        [StringLength(100)]
        public string NomeContrato { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtTipoIntervencao
    {
        public int CodTipoIntervencao { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}

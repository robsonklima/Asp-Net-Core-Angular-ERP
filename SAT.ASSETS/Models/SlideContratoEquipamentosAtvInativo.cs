using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("SlideContrato_EquipamentosAtvInativos")]
    public partial class SlideContratoEquipamentosAtvInativo
    {
        public int? CodEquipContrato { get; set; }
    }
}

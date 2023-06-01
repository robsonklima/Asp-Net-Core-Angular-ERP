using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DistribuicaodeChamadospeloParqueNew")]
    public partial class DistribuicaodeChamadospeloParqueNew
    {
        public int? QtdEquipamentos { get; set; }
        public int? QtdChamados { get; set; }
        public int? CodCliente { get; set; }
    }
}

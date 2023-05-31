using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class ItensPendente
    {
        public string NomeEquip { get; set; }
        public string NomePeca { get; set; }
        public int? Qtd { get; set; }
        public double? Parque { get; set; }
    }
}

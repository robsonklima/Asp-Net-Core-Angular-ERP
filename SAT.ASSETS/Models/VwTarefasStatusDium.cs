using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwTarefasStatusDium
    {
        public int? Quantidade { get; set; }
        [Required]
        [StringLength(8)]
        public string Status { get; set; }
    }
}

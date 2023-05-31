using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcContratoModelo
    {
        public int CodContrato { get; set; }
        [StringLength(20)]
        public string NroContrato { get; set; }
        public int CodEquip { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
    }
}

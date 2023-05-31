using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcModeloPosBanrisul
    {
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Required]
        [Column("CodEEquip")]
        [StringLength(30)]
        public string CodEequip { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeEquip { get; set; }
        [StringLength(100)]
        public string DescEquip { get; set; }
    }
}

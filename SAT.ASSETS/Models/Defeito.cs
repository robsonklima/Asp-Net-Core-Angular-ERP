using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Defeito")]
    public partial class Defeito
    {
        public Defeito()
        {
            DefeitoPos = new HashSet<DefeitoPo>();
        }

        [Key]
        public int CodDefeito { get; set; }
        [Column("CodEDefeito")]
        [StringLength(3)]
        public string CodEdefeito { get; set; }
        [StringLength(50)]
        public string NomeDefeito { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(DefeitoPo.CodDefeitoNavigation))]
        public virtual ICollection<DefeitoPo> DefeitoPos { get; set; }
    }
}

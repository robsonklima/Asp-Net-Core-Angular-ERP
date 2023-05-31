using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ItemReparoRede")]
    public partial class ItemReparoRede
    {
        public ItemReparoRede()
        {
            LaboratorioPosreparos = new HashSet<LaboratorioPosreparo>();
        }

        [Key]
        public int CodItemReparoRede { get; set; }
        public int CodItemRede { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeItemReparoRede { get; set; }
        [Required]
        [StringLength(50)]
        public string CodPertoItem { get; set; }

        [InverseProperty(nameof(LaboratorioPosreparo.CodItemReparoRedeNavigation))]
        public virtual ICollection<LaboratorioPosreparo> LaboratorioPosreparos { get; set; }
    }
}

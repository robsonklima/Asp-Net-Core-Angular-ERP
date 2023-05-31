using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class DespesaItemAlertum
    {
        public DespesaItemAlertum()
        {
            DespesaItems = new HashSet<DespesaItem>();
        }

        [Key]
        public int CodDespesaItemAlerta { get; set; }
        [Required]
        [StringLength(500)]
        public string DescItemAlerta { get; set; }

        [InverseProperty(nameof(DespesaItem.CodDespesaItemAlertaNavigation))]
        public virtual ICollection<DespesaItem> DespesaItems { get; set; }
    }
}

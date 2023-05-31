using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Layout")]
    public partial class Layout
    {
        public Layout()
        {
            LayoutCampos = new HashSet<LayoutCampo>();
        }

        [Key]
        public int CodLayout { get; set; }
        [Required]
        [StringLength(25)]
        public string NomeLayout { get; set; }
        public int CodCliente { get; set; }

        [InverseProperty(nameof(LayoutCampo.CodLayoutNavigation))]
        public virtual ICollection<LayoutCampo> LayoutCampos { get; set; }
    }
}

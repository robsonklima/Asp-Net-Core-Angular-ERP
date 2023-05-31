using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusProducaoPOS")]
    public partial class StatusProducaoPo
    {
        public StatusProducaoPo()
        {
            ProducaoPos = new HashSet<ProducaoPo>();
        }

        [Key]
        [Column("CodStatusProducaoPOS")]
        public int CodStatusProducaoPos { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(50)]
        public string Icon { get; set; }
        [StringLength(50)]
        public string Background { get; set; }

        [InverseProperty(nameof(ProducaoPo.CodStatusProducaoPosNavigation))]
        public virtual ICollection<ProducaoPo> ProducaoPos { get; set; }
    }
}

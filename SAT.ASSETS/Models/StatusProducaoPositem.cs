using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusProducaoPOSItem")]
    public partial class StatusProducaoPositem
    {
        public StatusProducaoPositem()
        {
            ProducaoPositems = new HashSet<ProducaoPositem>();
        }

        [Key]
        [Column("CodStatusProducaoPOSItem")]
        public int CodStatusProducaoPositem { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        [InverseProperty(nameof(ProducaoPositem.CodStatusProducaoPositemNavigation))]
        public virtual ICollection<ProducaoPositem> ProducaoPositems { get; set; }
    }
}

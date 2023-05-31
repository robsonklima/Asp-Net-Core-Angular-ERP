using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusFaturamentoSicredi")]
    public partial class StatusFaturamentoSicredi
    {
        public StatusFaturamentoSicredi()
        {
            FaturamentoSicredis = new HashSet<FaturamentoSicredi>();
        }

        [Key]
        public int CodStatusFaturamentoSicredi { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeStatusFaturamentoSicredi { get; set; }
        [Required]
        [StringLength(50)]
        public string CorStatusFaturamentoSicredi { get; set; }

        [InverseProperty(nameof(FaturamentoSicredi.CodStatusFaturamentoSicrediNavigation))]
        public virtual ICollection<FaturamentoSicredi> FaturamentoSicredis { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATPecasStatus")]
    public partial class RatpecasStatus
    {
        public RatpecasStatus()
        {
            RatdetalhesPecasStatuses = new HashSet<RatdetalhesPecasStatus>();
        }

        [Key]
        [Column("CodRATPecasStatus")]
        public int CodRatpecasStatus { get; set; }
        [StringLength(50)]
        public string Descricao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }

        [InverseProperty(nameof(RatdetalhesPecasStatus.CodRatpecasStatusNavigation))]
        public virtual ICollection<RatdetalhesPecasStatus> RatdetalhesPecasStatuses { get; set; }
    }
}

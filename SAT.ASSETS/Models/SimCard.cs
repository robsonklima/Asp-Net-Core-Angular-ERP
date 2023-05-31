using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SimCard")]
    public partial class SimCard
    {
        public SimCard()
        {
            Gps = new HashSet<Gp>();
        }

        [Key]
        public int CodSimCard { get; set; }
        [Required]
        [StringLength(20)]
        public string SimCardNumber { get; set; }
        [Required]
        [Column("TrackerID")]
        [StringLength(14)]
        public string TrackerId { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(Gp.CodSimCardNavigation))]
        public virtual ICollection<Gp> Gps { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcSimCardLivre
    {
        public int CodSimCard { get; set; }
        [Required]
        [StringLength(20)]
        public string SimCardNumber { get; set; }
        [Required]
        [Column("TrackerID")]
        [StringLength(14)]
        public string TrackerId { get; set; }
        public byte IndAtivo { get; set; }
    }
}

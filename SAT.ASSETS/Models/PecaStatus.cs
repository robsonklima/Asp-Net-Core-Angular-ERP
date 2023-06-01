using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PecaStatus")]
    public partial class PecaStatus
    {
        public PecaStatus()
        {
            Pecas = new HashSet<Peca>();
        }

        [Key]
        public int CodPecaStatus { get; set; }
        [Required]
        [StringLength(1)]
        public string SiglaStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeStatus { get; set; }
        [StringLength(250)]
        public string MsgStatus { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(Peca.CodPecaStatusNavigation))]
        public virtual ICollection<Peca> Pecas { get; set; }
    }
}

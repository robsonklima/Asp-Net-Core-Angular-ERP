using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoPecaStatus")]
    public partial class PedidoPecaStatus
    {
        public PedidoPecaStatus()
        {
            PedidoPecas = new HashSet<PedidoPeca>();
        }

        [Key]
        public int CodPedidoPecaStatus { get; set; }
        [Required]
        [StringLength(3)]
        public string SiglaStatus { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeStatus { get; set; }
        public int? CodTraducao { get; set; }

        [InverseProperty(nameof(PedidoPeca.CodPedidoPecaStatusNavigation))]
        public virtual ICollection<PedidoPeca> PedidoPecas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class PedidoPecaOb
    {
        [Key]
        public int CodPedidoPecaObs { get; set; }
        public int CodPedidoPeca { get; set; }
        [Required]
        [StringLength(500)]
        public string Observacao { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodPedido { get; set; }
        public int? CodPeca { get; set; }

        [ForeignKey(nameof(CodPedidoPeca))]
        [InverseProperty(nameof(PedidoPeca.PedidoPecaObs))]
        public virtual PedidoPeca CodPedidoPecaNavigation { get; set; }
    }
}

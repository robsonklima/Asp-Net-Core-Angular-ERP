using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PedidoPecaPCP")]
    public partial class PedidoPecaPcp
    {
        [Key]
        [Column("CodPedidoPecaPCP")]
        public int CodPedidoPecaPcp { get; set; }
        public int CodPedidoPeca { get; set; }
        public int? QtdEntregueConfirmada { get; set; }
        [StringLength(100)]
        public string Observacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        public byte IndAtivo { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodPedido { get; set; }
        public int? CodPeca { get; set; }

        [ForeignKey(nameof(CodPedidoPeca))]
        [InverseProperty(nameof(PedidoPeca.PedidoPecaPcps))]
        public virtual PedidoPeca CodPedidoPecaNavigation { get; set; }
    }
}

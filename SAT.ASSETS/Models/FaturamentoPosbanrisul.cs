using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FaturamentoPOSBanrisul")]
    public partial class FaturamentoPosbanrisul
    {
        [Key]
        [Column("CodFaturamentoPOSBanrisul")]
        public int CodFaturamentoPosbanrisul { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFaturamento { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorFaturado { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.FaturamentoPosbanrisuls))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}

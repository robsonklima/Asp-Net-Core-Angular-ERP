using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("VioladosPOSBanrisulDesconsiderado")]
    public partial class VioladosPosbanrisulDesconsiderado
    {
        [Key]
        [Column("CodVioladosPOSBanrisulDesconsiderados")]
        public int CodVioladosPosbanrisulDesconsiderados { get; set; }
        [Column("CodVioladosPOSBanrisul")]
        public int CodVioladosPosbanrisul { get; set; }
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
        public int Quantidade { get; set; }

        [ForeignKey(nameof(CodVioladosPosbanrisul))]
        [InverseProperty(nameof(VioladosPosbanrisul.VioladosPosbanrisulDesconsiderados))]
        public virtual VioladosPosbanrisul CodVioladosPosbanrisulNavigation { get; set; }
    }
}

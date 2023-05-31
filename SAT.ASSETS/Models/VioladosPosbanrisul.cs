using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("VioladosPOSBanrisul")]
    public partial class VioladosPosbanrisul
    {
        public VioladosPosbanrisul()
        {
            VioladosPosbanrisulDesconsiderados = new HashSet<VioladosPosbanrisulDesconsiderado>();
        }

        [Key]
        [Column("CodVioladosPOSBanrisul")]
        public int CodVioladosPosbanrisul { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        public int Incidentes { get; set; }
        public int Violados { get; set; }
        public int AguardandoCliente { get; set; }
        public int VioladosMesmoDia { get; set; }
        [Column("VioladosDN")]
        public int VioladosDn { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "text")]
        public string Observacao { get; set; }
        [Column("HD")]
        public int? Hd { get; set; }
        [Column("CDS")]
        public int? Cds { get; set; }
        public int? Perto { get; set; }
        [Column("FSC")]
        public int? Fsc { get; set; }
        [Column("FPR")]
        public int? Fpr { get; set; }
        public int? Solutech { get; set; }
        [StringLength(150)]
        public string IntervaloReferencia { get; set; }
        [StringLength(50)]
        public string MesReferencia { get; set; }

        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.VioladosPosbanrisuls))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
        [InverseProperty(nameof(VioladosPosbanrisulDesconsiderado.CodVioladosPosbanrisulNavigation))]
        public virtual ICollection<VioladosPosbanrisulDesconsiderado> VioladosPosbanrisulDesconsiderados { get; set; }
    }
}

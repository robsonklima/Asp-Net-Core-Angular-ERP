using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SolicPecaItem")]
    public partial class SolicPecaItem
    {
        public SolicPecaItem()
        {
            SolicPecaNfs = new HashSet<SolicPecaNf>();
        }

        [Key]
        public int CodSolicPecaItem { get; set; }
        public int? CodSolicPeca { get; set; }
        public byte? StatusSolicPecaItem { get; set; }
        public int? CodPeca { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataSolicPecaItem { get; set; }
        [StringLength(20)]
        public string CodUsuarioSolicPecaItem { get; set; }
        [StringLength(300)]
        public string MotivoCancelamento { get; set; }
        [StringLength(20)]
        public string DataManutencao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        public int? QtdePeca { get; set; }
        public int? QtdeLib { get; set; }
        public int? QtdeLibLast { get; set; }
        [Column("IndSolicNFItem")]
        public byte? IndSolicNfitem { get; set; }
        public int? QtdeEntreg { get; set; }

        [ForeignKey(nameof(CodSolicPeca))]
        [InverseProperty(nameof(SolicPeca.SolicPecaItems))]
        public virtual SolicPeca CodSolicPecaNavigation { get; set; }
        [InverseProperty(nameof(SolicPecaNf.CodSolicPecaItemNavigation))]
        public virtual ICollection<SolicPecaNf> SolicPecaNfs { get; set; }
    }
}

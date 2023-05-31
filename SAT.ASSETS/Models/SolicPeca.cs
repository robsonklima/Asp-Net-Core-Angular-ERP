using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SolicPeca")]
    public partial class SolicPeca
    {
        public SolicPeca()
        {
            SolicPecaItems = new HashSet<SolicPecaItem>();
            SolicPecaNfs = new HashSet<SolicPecaNf>();
        }

        [Key]
        public int CodSolicPeca { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodFilial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataSolic { get; set; }
        [Column("IndSolicNF")]
        public byte? IndSolicNf { get; set; }
        [StringLength(20)]
        public string CodUsuarioSolic { get; set; }

        [InverseProperty(nameof(SolicPecaItem.CodSolicPecaNavigation))]
        public virtual ICollection<SolicPecaItem> SolicPecaItems { get; set; }
        [InverseProperty(nameof(SolicPecaNf.CodSolicPecaNavigation))]
        public virtual ICollection<SolicPecaNf> SolicPecaNfs { get; set; }
    }
}

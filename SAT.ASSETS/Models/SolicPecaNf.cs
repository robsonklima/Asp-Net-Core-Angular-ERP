using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SolicPecaNF")]
    public partial class SolicPecaNf
    {
        [Key]
        public int CodSolicPeca { get; set; }
        [Key]
        [Column("CodNF")]
        public int CodNf { get; set; }
        [Key]
        public int CodSolicPecaItem { get; set; }
        public int? QtdeLib { get; set; }
        [Column("IndNFPecaLiberada")]
        public byte? IndNfpecaLiberada { get; set; }

        [ForeignKey(nameof(CodSolicPecaItem))]
        [InverseProperty(nameof(SolicPecaItem.SolicPecaNfs))]
        public virtual SolicPecaItem CodSolicPecaItemNavigation { get; set; }
        [ForeignKey(nameof(CodSolicPeca))]
        [InverseProperty(nameof(SolicPeca.SolicPecaNfs))]
        public virtual SolicPeca CodSolicPecaNavigation { get; set; }
    }
}

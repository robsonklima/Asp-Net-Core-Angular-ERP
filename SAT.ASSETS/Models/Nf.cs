using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("NF")]
    public partial class Nf
    {
        public Nf()
        {
            OsbancadaPecasNfs = new HashSet<OsbancadaPecasNf>();
        }

        [Key]
        [Column("CodNF")]
        public int CodNf { get; set; }
        public int? CodTransportadora { get; set; }
        [Column("CodNFPerto")]
        [StringLength(20)]
        public string CodNfperto { get; set; }
        [Column("DatNFPerto", TypeName = "datetime")]
        public DateTime? DatNfperto { get; set; }
        [StringLength(20)]
        public string CodUsuarioManutencao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUltimaManut { get; set; }
        [Column("DataSolicNF", TypeName = "datetime")]
        public DateTime? DataSolicNf { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodTecnico { get; set; }
        [StringLength(20)]
        public string NumConhecimento { get; set; }
        public byte? IndOrcamento { get; set; }
        [Column("IndOSBancada")]
        public byte? IndOsbancada { get; set; }
        public int? Volume { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? Peso { get; set; }
        [StringLength(20)]
        public string CodUsuarioRequisitante { get; set; }

        [ForeignKey(nameof(CodTransportadora))]
        [InverseProperty(nameof(Transportadora.Nfs))]
        public virtual Transportadora CodTransportadoraNavigation { get; set; }
        [InverseProperty(nameof(OsbancadaPecasNf.CodNfNavigation))]
        public virtual ICollection<OsbancadaPecasNf> OsbancadaPecasNfs { get; set; }
    }
}

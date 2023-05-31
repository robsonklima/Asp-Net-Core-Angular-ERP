using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("RATDetalhesPecasStatus")]
    public partial class RatdetalhesPecasStatus
    {
        [Key]
        [Column("CodRATDetalhesPecasStatus")]
        public int CodRatdetalhesPecasStatus { get; set; }
        [Column("CodRATDetalhesPecas")]
        public int CodRatdetalhesPecas { get; set; }
        [Column("CodRATPecasStatus")]
        public int CodRatpecasStatus { get; set; }
        public string Descricao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(50)]
        public string Transportadora { get; set; }
        [StringLength(50)]
        public string NroMinuta { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataPrevisao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEmbarque { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataChegada { get; set; }
        [Column("NroNF")]
        [StringLength(50)]
        public string NroNf { get; set; }

        [ForeignKey(nameof(CodRatpecasStatus))]
        [InverseProperty(nameof(RatpecasStatus.RatdetalhesPecasStatuses))]
        public virtual RatpecasStatus CodRatpecasStatusNavigation { get; set; }
    }
}

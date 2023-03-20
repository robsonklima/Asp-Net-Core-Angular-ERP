using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SAT.MODELS.Entities
{
    [Table("RATPecasStatus")]
    public partial class RelatorioAtendimentoPecaStatus
    {
        [Key]
        [Column("CodRATPecasStatus")]
        public int CodRatpecasStatus { get; set; }
        [StringLength(50)]
        public string Descricao { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }

        //[InverseProperty(nameof(RelatorioAtendimentoDetalhePeca.CodRatpecasStatusNavigation))]
        //public virtual ICollection<RelatorioAtendimentoDetalhePeca> RatdetalhesPecasStatuses { get; set; }
    }
}

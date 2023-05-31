using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoIncidenteNaoProcedente")]
    public partial class MotivoIncidenteNaoProcedente
    {
        public MotivoIncidenteNaoProcedente()
        {
            IncidenteNaoProcedentes = new HashSet<IncidenteNaoProcedente>();
        }

        [Key]
        public int CodMotivoIncidenteNaoProcedente { get; set; }
        [Required]
        [Column("MotivoIncidenteNaoProcedente")]
        [StringLength(200)]
        public string MotivoIncidenteNaoProcedente1 { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [InverseProperty(nameof(IncidenteNaoProcedente.CodMotivoIncidenteNaoProcedenteNavigation))]
        public virtual ICollection<IncidenteNaoProcedente> IncidenteNaoProcedentes { get; set; }
    }
}

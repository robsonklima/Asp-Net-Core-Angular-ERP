using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LaboratorioPOS")]
    public partial class LaboratorioPo
    {
        public LaboratorioPo()
        {
            LaboratorioPositems = new HashSet<LaboratorioPositem>();
        }

        [Key]
        [Column("CodLaboratorioPOS")]
        public int CodLaboratorioPos { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroDocumento { get; set; }
        public int TipoMovimentacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataMovimentacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column("CodStatusLaboratorioPOS")]
        public int CodStatusLaboratorioPos { get; set; }
        [StringLength(1000)]
        public string Erro { get; set; }
        [StringLength(50)]
        public string Destino { get; set; }

        [ForeignKey(nameof(CodStatusLaboratorioPos))]
        [InverseProperty(nameof(StatusLaboratorioPo.LaboratorioPos))]
        public virtual StatusLaboratorioPo CodStatusLaboratorioPosNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.LaboratorioPos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
        [InverseProperty(nameof(LaboratorioPositem.CodLaboratorioPosNavigation))]
        public virtual ICollection<LaboratorioPositem> LaboratorioPositems { get; set; }
    }
}

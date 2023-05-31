using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LaboratorioPOSItem")]
    public partial class LaboratorioPositem
    {
        public LaboratorioPositem()
        {
            LaboratorioPosreparos = new HashSet<LaboratorioPosreparo>();
        }

        [Key]
        [Column("CodLaboratorioPOSItem")]
        public int CodLaboratorioPositem { get; set; }
        [Column("CodLaboratorioPOS")]
        public int CodLaboratorioPos { get; set; }
        public long NumeroSerie { get; set; }
        [Required]
        [StringLength(50)]
        public string NumeroLogico { get; set; }
        public int CodProduto { get; set; }
        [StringLength(100)]
        public string Erro { get; set; }
        [Column("CodStatusLaboratorioPOSItem")]
        public int CodStatusLaboratorioPositem { get; set; }
        [StringLength(50)]
        public string TipoReparo { get; set; }

        [ForeignKey(nameof(CodLaboratorioPos))]
        [InverseProperty(nameof(LaboratorioPo.LaboratorioPositems))]
        public virtual LaboratorioPo CodLaboratorioPosNavigation { get; set; }
        [ForeignKey(nameof(CodProduto))]
        [InverseProperty(nameof(Produto.LaboratorioPositems))]
        public virtual Produto CodProdutoNavigation { get; set; }
        [ForeignKey(nameof(CodStatusLaboratorioPositem))]
        [InverseProperty(nameof(StatusLaboratorioPositem.LaboratorioPositems))]
        public virtual StatusLaboratorioPositem CodStatusLaboratorioPositemNavigation { get; set; }
        [InverseProperty(nameof(LaboratorioPosreparo.CodLaboratorioPositemNavigation))]
        public virtual ICollection<LaboratorioPosreparo> LaboratorioPosreparos { get; set; }
    }
}

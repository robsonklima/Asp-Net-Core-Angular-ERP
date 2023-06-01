using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoCancelamento")]
    public partial class MotivoCancelamento
    {
        public MotivoCancelamento()
        {
            Chamados = new HashSet<Chamado>();
            FecharOspos = new HashSet<FecharOspo>();
            Os = new HashSet<O>();
        }

        [Key]
        public int CodMotivoCancelamento { get; set; }
        [Required]
        [Column("MotivoCancelamento")]
        [StringLength(200)]
        public string MotivoCancelamento1 { get; set; }
        public bool GeraNotaServico { get; set; }
        public bool Ativo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }
        [Column("POS")]
        public bool Pos { get; set; }

        [InverseProperty("CodMotivoCancelamentoNavigation")]
        public virtual MotivoCancelamentoDePara MotivoCancelamentoDePara { get; set; }
        [InverseProperty(nameof(Chamado.CodMotivoCancelamentoNavigation))]
        public virtual ICollection<Chamado> Chamados { get; set; }
        [InverseProperty(nameof(FecharOspo.CodMotivoCancelamentoNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
        [InverseProperty(nameof(O.CodMotivoCancelamentoNavigation))]
        public virtual ICollection<O> Os { get; set; }
    }
}

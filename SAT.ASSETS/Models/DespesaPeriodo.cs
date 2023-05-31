using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaPeriodo")]
    public partial class DespesaPeriodo
    {
        public DespesaPeriodo()
        {
            DespesaAdiantamentoPeriodos = new HashSet<DespesaAdiantamentoPeriodo>();
            DespesaPeriodoTecnicos = new HashSet<DespesaPeriodoTecnico>();
            Despesas = new HashSet<Despesa>();
        }

        [Key]
        public int CodDespesaPeriodo { get; set; }
        public int CodDespesaConfiguracao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey(nameof(CodDespesaConfiguracao))]
        [InverseProperty(nameof(DespesaConfiguracao.DespesaPeriodos))]
        public virtual DespesaConfiguracao CodDespesaConfiguracaoNavigation { get; set; }
        [InverseProperty(nameof(DespesaAdiantamentoPeriodo.CodDespesaPeriodoNavigation))]
        public virtual ICollection<DespesaAdiantamentoPeriodo> DespesaAdiantamentoPeriodos { get; set; }
        [InverseProperty(nameof(DespesaPeriodoTecnico.CodDespesaPeriodoNavigation))]
        public virtual ICollection<DespesaPeriodoTecnico> DespesaPeriodoTecnicos { get; set; }
        [InverseProperty(nameof(Despesa.CodDespesaPeriodoNavigation))]
        public virtual ICollection<Despesa> Despesas { get; set; }
    }
}

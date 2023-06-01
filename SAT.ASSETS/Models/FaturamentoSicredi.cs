using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FaturamentoSicredi")]
    public partial class FaturamentoSicredi
    {
        public FaturamentoSicredi()
        {
            FaturamentoSicrediMultaSaldos = new HashSet<FaturamentoSicrediMultaSaldo>();
        }

        [Key]
        public int CodFaturamentoSicredi { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataMesAnoReferencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataGeracao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodStatusFaturamentoSicredi { get; set; }
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }

        [ForeignKey(nameof(CodStatusFaturamentoSicredi))]
        [InverseProperty(nameof(StatusFaturamentoSicredi.FaturamentoSicredis))]
        public virtual StatusFaturamentoSicredi CodStatusFaturamentoSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.FaturamentoSicredis))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldo.CodFaturamentoSicrediNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldo> FaturamentoSicrediMultaSaldos { get; set; }
    }
}

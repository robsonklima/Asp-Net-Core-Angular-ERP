using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSBancadaPecasOrcamento")]
    public partial class OsbancadaPecasOrcamento
    {
        public OsbancadaPecasOrcamento()
        {
            OrcamentoPecasEspecs = new HashSet<OrcamentoPecasEspec>();
        }

        [Key]
        public int CodOrcamento { get; set; }
        [Key]
        [Column("CodOSBancada")]
        public int CodOsbancada { get; set; }
        [Key]
        [Column("CodPecaRE5114")]
        public int CodPecaRe5114 { get; set; }
        public byte? IndOrcamentoAprov { get; set; }
        public byte? TipoOrcamentoEscolhido { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorPreAprovado { get; set; }
        [StringLength(500)]
        public string Observacao { get; set; }
        public int? NumeroAlteracao { get; set; }
        public int? CodOrcamentoPai { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [StringLength(20)]
        public string NumOrdemCompra { get; set; }
        [StringLength(100)]
        public string MotivoReprov { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public int? CodOrcamentoQtdPai { get; set; }

        [ForeignKey("CodOsbancada,CodPecaRe5114")]
        [InverseProperty(nameof(OsbancadaPeca.OsbancadaPecasOrcamentos))]
        public virtual OsbancadaPeca Cod { get; set; }
        [InverseProperty(nameof(OrcamentoPecasEspec.Cod))]
        public virtual ICollection<OrcamentoPecasEspec> OrcamentoPecasEspecs { get; set; }
    }
}

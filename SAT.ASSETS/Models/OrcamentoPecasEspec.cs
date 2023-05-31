using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentoPecasEspec")]
    public partial class OrcamentoPecasEspec
    {
        [Key]
        public int CodOrcamentoPecasEspec { get; set; }
        public int? CodPeca { get; set; }
        public int? CodOrcamento { get; set; }
        [Column("CodOSBancada")]
        public int? CodOsbancada { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? Quantidade { get; set; }
        [Column("CodPecaRE5114")]
        public int? CodPecaRe5114 { get; set; }
        public byte? IndCobranca { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorPeca { get; set; }
        public byte? TipoDesconto { get; set; }
        public int? CodBancadaLista { get; set; }
        [Column(TypeName = "decimal(10, 4)")]
        public decimal? ValorDesconto { get; set; }
        [Column("PercIPI", TypeName = "decimal(10, 4)")]
        public decimal? PercIpi { get; set; }

        [ForeignKey("CodOrcamento,CodOsbancada,CodPecaRe5114")]
        [InverseProperty(nameof(OsbancadaPecasOrcamento.OrcamentoPecasEspecs))]
        public virtual OsbancadaPecasOrcamento Cod { get; set; }
    }
}

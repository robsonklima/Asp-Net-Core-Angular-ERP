using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwPedidoPecaComDescontoOld
    {
        public int CodPedidoPeca { get; set; }
        public int CodPedido { get; set; }
        public int CodPeca { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEntregaProgramada { get; set; }
        public int QtdSolicitada { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorDefault { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Valor { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Desconto { get; set; }
        public int? CodTipoDesconto { get; set; }
        [StringLength(2)]
        public string SiglaTipoDesc { get; set; }
        [Column("ValIPI", TypeName = "decimal(10, 2)")]
        public decimal ValIpi { get; set; }
        public int QtdMinimaVenda { get; set; }
        [Column("QtdDiaPrevEntregaPCP")]
        public int? QtdDiaPrevEntregaPcp { get; set; }
        public int? QtdDiaPrevEntregaCliente { get; set; }
        public int? CodPedidoPecaStatus { get; set; }
        [StringLength(3)]
        public string SiglaStatus { get; set; }
        [StringLength(30)]
        public string NomeStatus { get; set; }
        [StringLength(1)]
        public string IndObs { get; set; }
        [Column("PercICMS", TypeName = "decimal(10, 2)")]
        public decimal? PercIcms { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercAjuste { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorComDesconto { get; set; }
        [Column(TypeName = "decimal(21, 2)")]
        public decimal? ValorTotal { get; set; }
        public int? QtdEntregueConfirmadaTotal { get; set; }
    }
}

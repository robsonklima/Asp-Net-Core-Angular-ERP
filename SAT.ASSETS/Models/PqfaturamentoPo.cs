using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("'PQFaturamentoPOS'")]
    public partial class PqfaturamentoPo
    {
        [Column("num_pedido_repres")]
        [StringLength(255)]
        public string NumPedidoRepres { get; set; }
        [Column("nom_filial")]
        [StringLength(255)]
        public string NomFilial { get; set; }
        [Column("dat_refer", TypeName = "datetime")]
        public DateTime? DatRefer { get; set; }
        [Column("num_pedido")]
        [StringLength(255)]
        public string NumPedido { get; set; }
        [Column("num_nff")]
        [StringLength(255)]
        public string NumNff { get; set; }
        [Column("cod_cliente")]
        [StringLength(255)]
        public string CodCliente { get; set; }
        [Column("nom_cliente")]
        [StringLength(255)]
        public string NomCliente { get; set; }
        [Column("cod_item")]
        [StringLength(255)]
        public string CodItem { get; set; }
        [Column("nom_item")]
        [StringLength(255)]
        public string NomItem { get; set; }
        [Column("tip_fat")]
        [StringLength(255)]
        public string TipFat { get; set; }
        [Column("qtd_item")]
        public double? QtdItem { get; set; }
        [Column("val_fatur")]
        public double? ValFatur { get; set; }
    }
}

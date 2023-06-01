using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoEstoque")]
    public partial class LoEstoque
    {
        [Required]
        [Column("cod_empresa")]
        [StringLength(2)]
        public string CodEmpresa { get; set; }
        [Required]
        [Column("cod_item")]
        [StringLength(15)]
        public string CodItem { get; set; }
        [Column("qtd_liberada", TypeName = "decimal(15, 3)")]
        public decimal? QtdLiberada { get; set; }
        [Column("qtd_impedida", TypeName = "decimal(15, 3)")]
        public decimal? QtdImpedida { get; set; }
        [Column("qtd_rejeitada", TypeName = "decimal(15, 3)")]
        public decimal? QtdRejeitada { get; set; }
        [Column("qtd_lib_excep", TypeName = "decimal(15, 3)")]
        public decimal? QtdLibExcep { get; set; }
        [Column("qtd_disp_venda", TypeName = "decimal(15, 3)")]
        public decimal? QtdDispVenda { get; set; }
        [Column("qtd_reservada", TypeName = "decimal(15, 3)")]
        public decimal? QtdReservada { get; set; }
        [Column("dat_ult_invent")]
        [StringLength(50)]
        public string DatUltInvent { get; set; }
        [Column("dat_ult_entrada")]
        [StringLength(50)]
        public string DatUltEntrada { get; set; }
        [Column("dat_ult_saida")]
        [StringLength(50)]
        public string DatUltSaida { get; set; }
    }
}

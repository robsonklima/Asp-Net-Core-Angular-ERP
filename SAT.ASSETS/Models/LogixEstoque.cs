using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LogixEstoque")]
    public partial class LogixEstoque
    {
        [Key]
        [Column("codLogixEstoque")]
        public int CodLogixEstoque { get; set; }
        [Key]
        [Column("cod_empresa")]
        public int CodEmpresa { get; set; }
        [Required]
        [Column("cod_item")]
        [StringLength(15)]
        public string CodItem { get; set; }
        [Column("qtd_liberada", TypeName = "decimal(15, 3)")]
        public decimal QtdLiberada { get; set; }
        [Column("qtd_impedida", TypeName = "decimal(15, 3)")]
        public decimal QtdImpedida { get; set; }
        [Column("qtd_rejeitada", TypeName = "decimal(15, 3)")]
        public decimal QtdRejeitada { get; set; }
        [Column("qtd_lib_excep", TypeName = "decimal(15, 3)")]
        public decimal QtdLibExcep { get; set; }
        [Column("qtd_disp_venda", TypeName = "decimal(15, 3)")]
        public decimal QtdDispVenda { get; set; }
        [Column("qtd_reservada", TypeName = "decimal(15, 3)")]
        public decimal QtdReservada { get; set; }
        [Column("dat_ult_invent")]
        [StringLength(10)]
        public string DatUltInvent { get; set; }
        [Column("dat_ult_entrada")]
        [StringLength(10)]
        public string DatUltEntrada { get; set; }
        [Column("dat_ult_saida")]
        [StringLength(10)]
        public string DatUltSaida { get; set; }
        [Column("dataHoraCadastro", TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
    }
}

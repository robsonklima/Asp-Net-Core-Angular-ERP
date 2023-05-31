using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoEstrutura")]
    public partial class LoEstrutura
    {
        [Required]
        [Column("cod_empresa")]
        [StringLength(50)]
        public string CodEmpresa { get; set; }
        [Required]
        [Column("cod_item_pai")]
        [StringLength(50)]
        public string CodItemPai { get; set; }
        [Required]
        [Column("cod_item_compon")]
        [StringLength(50)]
        public string CodItemCompon { get; set; }
        [Column("qtd_necessaria", TypeName = "decimal(14, 7)")]
        public decimal QtdNecessaria { get; set; }
        [Column("pct_refug", TypeName = "decimal(6, 3)")]
        public decimal PctRefug { get; set; }
        [Column("dat_validade_ini")]
        [StringLength(50)]
        public string DatValidadeIni { get; set; }
        [Column("dat_validade_fim")]
        [StringLength(50)]
        public string DatValidadeFim { get; set; }
        [Column("tmp_ressup_sobr", TypeName = "decimal(3, 0)")]
        public decimal TmpRessupSobr { get; set; }
        [Required]
        [Column("cod_cent_cust")]
        [StringLength(50)]
        public string CodCentCust { get; set; }
        [Column("cod_comp_custo")]
        [StringLength(50)]
        public string CodCompCusto { get; set; }
        [Column("parametros")]
        [StringLength(50)]
        public string Parametros { get; set; }
    }
}

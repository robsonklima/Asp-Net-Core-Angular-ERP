using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoItem")]
    public partial class LoItem
    {
        [Column("cod_empresa")]
        [StringLength(50)]
        public string CodEmpresa { get; set; }
        [Column("cod_item")]
        [StringLength(50)]
        public string CodItem { get; set; }
        [Column("den_item")]
        [StringLength(100)]
        public string DenItem { get; set; }
        [Column("den_item_reduz")]
        [StringLength(50)]
        public string DenItemReduz { get; set; }
        [Column("cod_unid_med")]
        [StringLength(50)]
        public string CodUnidMed { get; set; }
        [Column("pes_unit")]
        [StringLength(50)]
        public string PesUnit { get; set; }
        [Column("ies_tip_item")]
        [StringLength(50)]
        public string IesTipItem { get; set; }
        [Column("dat_cadastro")]
        [StringLength(50)]
        public string DatCadastro { get; set; }
        [Column("ies_ctr_estoque")]
        [StringLength(50)]
        public string IesCtrEstoque { get; set; }
        [Column("cod_local_estoq")]
        [StringLength(50)]
        public string CodLocalEstoq { get; set; }
        [Column("ies_tem_inspecao")]
        [StringLength(50)]
        public string IesTemInspecao { get; set; }
        [Column("cod_local_insp")]
        [StringLength(50)]
        public string CodLocalInsp { get; set; }
        [Column("ies_ctr_lote")]
        [StringLength(50)]
        public string IesCtrLote { get; set; }
        [Column("cod_familia")]
        [StringLength(50)]
        public string CodFamilia { get; set; }
        [Column("gru_ctr_estoq")]
        [StringLength(50)]
        public string GruCtrEstoq { get; set; }
        [Column("cod_cla_fisc")]
        [StringLength(50)]
        public string CodClaFisc { get; set; }
        [Column("pct_ipi")]
        [StringLength(50)]
        public string PctIpi { get; set; }
        [Column("cod_lin_prod")]
        [StringLength(50)]
        public string CodLinProd { get; set; }
        [Column("cod_lin_recei")]
        [StringLength(50)]
        public string CodLinRecei { get; set; }
        [Column("cod_seg_merc")]
        [StringLength(50)]
        public string CodSegMerc { get; set; }
        [Column("cod_cla_uso")]
        [StringLength(50)]
        public string CodClaUso { get; set; }
        [Column("fat_conver")]
        [StringLength(50)]
        public string FatConver { get; set; }
        [Column("ies_situacao")]
        [StringLength(50)]
        public string IesSituacao { get; set; }
    }
}

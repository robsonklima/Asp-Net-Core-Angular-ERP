using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoItemEmTerc")]
    public partial class LoItemEmTerc
    {
        [Column("cod_empresa")]
        [StringLength(50)]
        public string CodEmpresa { get; set; }
        [Column("num_nf")]
        [StringLength(100)]
        public string NumNf { get; set; }
        [Column("num_sequencia")]
        [StringLength(50)]
        public string NumSequencia { get; set; }
        [Column("dat_emis_nf")]
        [StringLength(50)]
        public string DatEmisNf { get; set; }
        [Column("cod_fornecedor")]
        [StringLength(50)]
        public string CodFornecedor { get; set; }
        [Column("ies_incl_contab")]
        [StringLength(50)]
        public string IesInclContab { get; set; }
        [Column("dat_inclusao_seq")]
        [StringLength(50)]
        public string DatInclusaoSeq { get; set; }
        [Column("cod_cla_fisc")]
        [StringLength(50)]
        public string CodClaFisc { get; set; }
        [Column("cod_item")]
        [StringLength(50)]
        public string CodItem { get; set; }
        [Column("den_item")]
        [StringLength(50)]
        public string DenItem { get; set; }
        [Column("cod_unid_med")]
        [StringLength(50)]
        public string CodUnidMed { get; set; }
        [Column("dat_emis_nf_usina")]
        [StringLength(50)]
        public string DatEmisNfUsina { get; set; }
        [Column("dat_retorno_prev")]
        [StringLength(50)]
        public string DatRetornoPrev { get; set; }
        [Column("cod_motivo_remessa")]
        [StringLength(50)]
        public string CodMotivoRemessa { get; set; }
        [Column("qtd_tot_remessa")]
        [StringLength(50)]
        public string QtdTotRemessa { get; set; }
        [Column("val_estoque")]
        [StringLength(50)]
        public string ValEstoque { get; set; }
        [Column("val_remessa")]
        [StringLength(50)]
        public string ValRemessa { get; set; }
        [Column("val_icms")]
        [StringLength(50)]
        public string ValIcms { get; set; }
        [Column("val_ipi")]
        [StringLength(50)]
        public string ValIpi { get; set; }
        [Column("qtd_tot_recebida")]
        [StringLength(50)]
        public string QtdTotRecebida { get; set; }
        [Column("cod_area_negocio")]
        [StringLength(50)]
        public string CodAreaNegocio { get; set; }
        [Column("cod_lin_negocio")]
        [StringLength(50)]
        public string CodLinNegocio { get; set; }
        [Column("num_conta")]
        [StringLength(50)]
        public string NumConta { get; set; }
        [Column("tex_observ")]
        [StringLength(50)]
        public string TexObserv { get; set; }
        [Column("prim_ped_prorrog")]
        [StringLength(50)]
        public string PrimPedProrrog { get; set; }
        [Column("prim_dat_vencto")]
        [StringLength(50)]
        public string PrimDatVencto { get; set; }
        [Column("seg_ped_prorrog")]
        [StringLength(50)]
        public string SegPedProrrog { get; set; }
        [Column("seg_dat_vencto")]
        [StringLength(50)]
        public string SegDatVencto { get; set; }
        [Column("terc_ped_prorrog")]
        [StringLength(50)]
        public string TercPedProrrog { get; set; }
        [Column("terc_dat_vencto")]
        [StringLength(50)]
        public string TercDatVencto { get; set; }
    }
}

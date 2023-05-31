using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoItemRetTerc")]
    public partial class LoItemRetTerc
    {
        [Column("cod_empresa")]
        [StringLength(50)]
        public string CodEmpresa { get; set; }
        [Column("num_nf")]
        [StringLength(50)]
        public string NumNf { get; set; }
        [Column("ser_nf")]
        [StringLength(50)]
        public string SerNf { get; set; }
        [Column("ssr_nf")]
        [StringLength(50)]
        public string SsrNf { get; set; }
        [Column("ies_especie_nf ")]
        [StringLength(50)]
        public string IesEspecieNf { get; set; }
        [Column("cod_fornecedor")]
        [StringLength(50)]
        public string CodFornecedor { get; set; }
        [Column("ies_incl_contab")]
        [StringLength(50)]
        public string IesInclContab { get; set; }
        [Column("num_sequencia_ar")]
        [StringLength(50)]
        public string NumSequenciaAr { get; set; }
        [Column("dat_emis_nf")]
        [StringLength(50)]
        public string DatEmisNf { get; set; }
        [Column("dat_entrada_nf")]
        [StringLength(50)]
        public string DatEntradaNf { get; set; }
        [Column("dat_inclusao_seq")]
        [StringLength(50)]
        public string DatInclusaoSeq { get; set; }
        [Column("num_nf_remessa")]
        [StringLength(50)]
        public string NumNfRemessa { get; set; }
        [Column("num_sequencia_nf")]
        [StringLength(50)]
        public string NumSequenciaNf { get; set; }
        [Column("qtd_devolvida")]
        [StringLength(50)]
        public string QtdDevolvida { get; set; }
        [Column("tex_observ ")]
        [StringLength(50)]
        public string TexObserv { get; set; }
        [Column("num_transac")]
        [StringLength(50)]
        public string NumTransac { get; set; }
    }
}

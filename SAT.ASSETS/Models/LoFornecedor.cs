using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("LoFornecedor")]
    public partial class LoFornecedor
    {
        [Column("num_cgc_cpf")]
        [StringLength(255)]
        public string NumCgcCpf { get; set; }
        [Column("cod_fornecedor")]
        public double? CodFornecedor { get; set; }
        [Column("raz_social")]
        [StringLength(255)]
        public string RazSocial { get; set; }
        [Column("raz_social_reduz")]
        [StringLength(255)]
        public string RazSocialReduz { get; set; }
        [Column("ies_tip_fornec")]
        public double? IesTipFornec { get; set; }
        [Column("ies_fornec_ativo")]
        [StringLength(255)]
        public string IesFornecAtivo { get; set; }
        [Column("ies_contrib_ipi")]
        [StringLength(255)]
        public string IesContribIpi { get; set; }
        [Column("ies_fis_juridica")]
        [StringLength(255)]
        public string IesFisJuridica { get; set; }
        [Column("ins_estadual")]
        public double? InsEstadual { get; set; }
        [Column("dat_cadast", TypeName = "datetime")]
        public DateTime? DatCadast { get; set; }
        [Column("dat_atualiz", TypeName = "datetime")]
        public DateTime? DatAtualiz { get; set; }
        [Column("dat_validade", TypeName = "datetime")]
        public DateTime? DatValidade { get; set; }
        [Column("dat_movto_ult", TypeName = "datetime")]
        public DateTime? DatMovtoUlt { get; set; }
        [Column("end_fornec")]
        [StringLength(255)]
        public string EndFornec { get; set; }
        [Column("den_bairro")]
        [StringLength(255)]
        public string DenBairro { get; set; }
        [Column("cod_cep")]
        [StringLength(255)]
        public string CodCep { get; set; }
        [Column("cod_cidade")]
        public double? CodCidade { get; set; }
        [Column("cod_uni_feder")]
        [StringLength(255)]
        public string CodUniFeder { get; set; }
        [Column("cod_pais")]
        public double? CodPais { get; set; }
        [Column("ies_zona_franca")]
        [StringLength(255)]
        public string IesZonaFranca { get; set; }
        [Column("num_telefone")]
        [StringLength(255)]
        public string NumTelefone { get; set; }
        [Column("num_fax")]
        [StringLength(255)]
        public string NumFax { get; set; }
        [Column("num_telex")]
        public double? NumTelex { get; set; }
        [Column("nom_contato")]
        [StringLength(255)]
        public string NomContato { get; set; }
        [Column("nom_guerra")]
        [StringLength(255)]
        public string NomGuerra { get; set; }
        [Column("cod_cidade_pgto")]
        [StringLength(255)]
        public string CodCidadePgto { get; set; }
        [Column("camara_comp")]
        [StringLength(255)]
        public string CamaraComp { get; set; }
        [Column("cod_banco")]
        [StringLength(255)]
        public string CodBanco { get; set; }
        [Column("num_agencia")]
        public double? NumAgencia { get; set; }
        [Column("num_conta_banco")]
        [StringLength(255)]
        public string NumContaBanco { get; set; }
        [Column("tmp_transpor")]
        [StringLength(255)]
        public string TmpTranspor { get; set; }
        [Column("tex_observ")]
        [StringLength(255)]
        public string TexObserv { get; set; }
        [Column("num_lote_transf")]
        [StringLength(255)]
        public string NumLoteTransf { get; set; }
        [Column("pct_aceite_div")]
        [StringLength(255)]
        public string PctAceiteDiv { get; set; }
        [Column("ies_tip_entrega")]
        [StringLength(255)]
        public string IesTipEntrega { get; set; }
        [Column("ies_dep_cred")]
        [StringLength(255)]
        public string IesDepCred { get; set; }
        [Column("ult_num_coleta")]
        [StringLength(255)]
        public string UltNumColeta { get; set; }
        [Column("ies_gera_ap")]
        [StringLength(255)]
        public string IesGeraAp { get; set; }
    }
}

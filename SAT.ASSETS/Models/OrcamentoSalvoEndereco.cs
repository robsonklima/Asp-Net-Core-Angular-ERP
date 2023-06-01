using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcamentoSalvoEndereco")]
    public partial class OrcamentoSalvoEndereco
    {
        [Key]
        public int CodOrcamentoSalvoEndereco { get; set; }
        public int? CodOrcamento { get; set; }
        [StringLength(500)]
        public string RazaoSocialFaturamento { get; set; }
        [Column("CEPFaturamento")]
        [StringLength(20)]
        public string Cepfaturamento { get; set; }
        [StringLength(100)]
        public string EnderecoFaturamento { get; set; }
        [StringLength(50)]
        public string ComplementoFaturamento { get; set; }
        [Column("CNPJFaturamento")]
        [StringLength(20)]
        public string Cnpjfaturamento { get; set; }
        [StringLength(15)]
        public string InscricaoEstadualFaturamento { get; set; }
        [StringLength(70)]
        public string ResponsavelFaturamento { get; set; }
        [StringLength(20)]
        public string NumeroFaturamento { get; set; }
        [StringLength(60)]
        public string BairroFaturamento { get; set; }
        [StringLength(70)]
        public string NomeCidadeFaturamento { get; set; }
        [Column("SiglaUFFaturamento")]
        [StringLength(2)]
        public string SiglaUffaturamento { get; set; }
        [StringLength(150)]
        public string EmailFaturamento { get; set; }
        [StringLength(20)]
        public string FoneFaturamento { get; set; }
        [StringLength(20)]
        public string FaxFaturamento { get; set; }
        [Column("RazaoSocialEnvioNF")]
        [StringLength(500)]
        public string RazaoSocialEnvioNf { get; set; }
        [Column("CEPEnvioNF")]
        [StringLength(20)]
        public string CepenvioNf { get; set; }
        [Column("EnderecoEnvioNF")]
        [StringLength(100)]
        public string EnderecoEnvioNf { get; set; }
        [Column("ComplementoEnvioNF")]
        [StringLength(50)]
        public string ComplementoEnvioNf { get; set; }
        [Column("CNPJEnvioNF")]
        [StringLength(20)]
        public string CnpjenvioNf { get; set; }
        [Column("InscricaoEstadualEnvioNF")]
        [StringLength(15)]
        public string InscricaoEstadualEnvioNf { get; set; }
        [Column("ResponsavelEnvioNF")]
        [StringLength(70)]
        public string ResponsavelEnvioNf { get; set; }
        [Column("NumeroEnvioNF")]
        [StringLength(20)]
        public string NumeroEnvioNf { get; set; }
        [Column("BairroEnvioNF")]
        [StringLength(60)]
        public string BairroEnvioNf { get; set; }
        [Column("NomeCidadeEnvioNF")]
        [StringLength(70)]
        public string NomeCidadeEnvioNf { get; set; }
        [Column("SiglaUFEnvioNF")]
        [StringLength(2)]
        public string SiglaUfenvioNf { get; set; }
        [Column("EmailEnvioNF")]
        [StringLength(150)]
        public string EmailEnvioNf { get; set; }
        [Column("FoneEnvioNF")]
        [StringLength(20)]
        public string FoneEnvioNf { get; set; }
        [Column("FaxEnvioNF")]
        [StringLength(20)]
        public string FaxEnvioNf { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        [StringLength(10)]
        public string Agencia { get; set; }
        [StringLength(150)]
        public string Endereco { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(30)]
        public string NumOscliente { get; set; }
        [StringLength(40)]
        public string NomeEquip { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [StringLength(20)]
        public string NumPatrimonio { get; set; }
        [StringLength(200)]
        public string EnderecoComplemento { get; set; }
        [StringLength(100)]
        public string Bairro { get; set; }
        [Column("CEP")]
        [StringLength(8)]
        public string Cep { get; set; }
        [StringLength(70)]
        public string NomeCidade { get; set; }
        [Column("UF")]
        [StringLength(2)]
        public string Uf { get; set; }
        [Column("CNPJ")]
        [StringLength(20)]
        public string Cnpj { get; set; }
    }
}

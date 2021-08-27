using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Cliente
    {
        [Key]
        public int CodCliente { get; set; }
        public int? CodFormaPagto { get; set; }
        public int? CodMoeda { get; set; }
        public int? CodTipoFrete { get; set; }
        public int? CodPecaLista { get; set; }
        public int? CodTransportadora { get; set; }
        [ForeignKey("CodTransportadora")]
        public Transportadora Transportadora { get; set; }
        public int? CodCidade { get; set; }
        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string NumBanco { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Observacao { get; set; }
        public decimal? Inflacao { get; set; }
        public decimal? Deflacao { get; set; }
        public decimal? PercIcms { get; set; }
        public byte? IndHabilitaVendaPecas { get; set; }
        public byte IndPecaListaSomente { get; set; }
        public byte? IndRevisao { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? TotalEquipAtivos { get; set; }
        public int? TotalEquipDeastivados { get; set; }
        public int? TotalEquip { get; set; }
        public string NumeroEnd { get; set; }
        public string ObsCliente { get; set; }
        public string ComplemEnd { get; set; }
        public string SiglaUF { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public int? CodTipoMercado { get; set; }
        public string CodUsuarioCoordenadorContrato { get; set; }
        public decimal? IcmsLab { get; set; }
        public decimal? InflacaoLab { get; set; }
        public string InflacaoObsLab { get; set; }
        public decimal? DeflacaoLab { get; set; }
        public string DeflacaoObsLab { get; set; }
        public int? CodPecaListaLab { get; set; }
        public int? CodFormaPagtoLab { get; set; }
        public int? CodTipoFreteLab { get; set; }
        public int? CodTransportadoraLab { get; set; }
        public byte? IndOrcamentoLab { get; set; }
    }
}

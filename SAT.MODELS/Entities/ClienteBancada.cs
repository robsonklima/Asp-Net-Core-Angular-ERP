using System;

namespace SAT.MODELS.Entities
{
    public class ClienteBancada
    {
        public int CodClienteBancada { get; set; }
        public int? CodCidade { get; set; }
        public string NomeCliente { get; set; }
        public string Apelido { get; set; }
        public string CNPJ_CGC { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complem { get; set; }
        public string Bairro { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Contato { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataManut { get; set; }
        public string Email { get; set; }
        public decimal? Icms { get; set; }
        public int? CodFormaPagto { get; set; }
        public int? CodTransportadora { get; set; }
        public byte? IndAtivo { get; set; }
        public decimal? Inflacao { get; set; }
        public string InflacaoObs { get; set; }
        public decimal? Deflacao { get; set; }
        public string DeflacaoObs { get; set; }
        public byte? CodTipoFrete { get; set; }
        public byte? IndOrcamento { get; set; }
        public byte? CodBancadaLista { get; set; }
        public Cidade Cidade { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class LocalAtendimento
    {
        public int CodPosto { get; set; }
        public int CodCliente { get; set; }
        public Cliente Cliente { get; set; }
        public string NomeLocal { get; set; }
        public string NumAgencia { get; set; }
        public string DCPosto { get; set; }
        public int? CodTipoRota { get; set; }
        public TipoRota TipoRota { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string EnderecoComplemento { get; set; }
        public string Bairro { get; set; }
        public int CodCidade { get; set; }
        public Cidade Cidade { get; set; }
        public string Email { get; set; }
        public string Site { get; set; }
        public string Fone { get; set; }
        public string Fax { get; set; }
        public string DescTurno { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string EnderecoCoordenadas { get; set; }
        public string BairroCoordenadas { get; set; }
        public string CidadeCoordenadas { get; set; }
        public string UfCoordenadas { get; set; }
        public string PaisCoordenadas { get; set; }
        [Column("DistanciaKmPAT_Res")]
        public decimal? DistanciaKmPatRes { get; set; }
        public string Observacao { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndPAE_DEL { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string NumeroEnd { get; set; }
        public int? CodFilial { get; set; }
        public Filial Filial { get; set; }
        public Autorizada Autorizada { get; set; }
        public int? CodAutorizada { get; set; }
        public Regiao Regiao { get; set; }
        public int? CodRegiao { get; set; }
        public string SiglaUf { get; set; }
        public int? CodRegional { get; set; }
        public string CnpjFaturamento { get; set; }
        public string SenhaAcessoNotaFiscal { get; set; }
        public string Telefone1_DEL { get; set; }
        public string Telefone2_DEL { get; set; }
    }
}

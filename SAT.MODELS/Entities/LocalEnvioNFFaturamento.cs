using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class LocalEnvioNFFaturamento {   
        public int CodLocalEnvioNFFaturamento { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public string RazaoSocialFaturamento { get; set; }
        public string EnderecoFaturamento { get; set; }
        public string ComplementoFaturamento { get; set; }
        public string NumeroFaturamento { get; set; }
        public string BairroFaturamento { get; set; }
        public string CnpjFaturamento { get; set; }
        public string InscricaoEstadualFaturamento { get; set; }
        public string ResponsavelFaturamento { get; set; }
        public string EmailFaturamento { get; set; }
        public string FoneFaturamento { get; set; }
        public string FaxFaturamento { get; set; }
        public byte? IndAtivoFaturamento { get; set; }
        public string CepFaturamento { get; set; }
        public int? CodUFFaturamento { get; set; }
        public int? CodCidadeFaturamento { get; set; }
        public string RazaoSocialEnvioNF { get; set; }
        public string EnderecoEnvioNF { get; set; }
        public string ComplementoEnvioNF { get; set; }
        public string NumeroEnvioNF { get; set; }
        public string BairroEnvioNF { get; set; }
        public string CnpjEnvioNF { get; set; }
        public string InscricaoEstadualEnvioNF { get; set; }
        public string ResponsavelEnvioNF { get; set; }
        public string EmailEnvioNF { get; set; }
        public string FoneEnvioNF { get; set; }
        public string FaxEnvioNF { get; set; }
        public byte? IndAtivoEnvioNF { get; set; }
        public string CepEnvioNF { get; set; }
        public int? CodCidadeEnvioNF { get; set; }
        public int? CodUFEnvioNF { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public Cliente Cliente { get; set; }
        public Contrato Contrato { get; set; }
        public Cidade CidadeFaturamento { get; set; }
        public Cidade CidadeEnvioNF { get; set; }
    }
}
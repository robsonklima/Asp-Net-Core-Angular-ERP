using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Filial
    {
        public int CodFilial { get; set; }
        public int CodCidade { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFilial { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Email { get; set; }
        public decimal? ICMS { get; set; }
        [ForeignKey("CodCidade")]
        public virtual Cidade Cidade { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
        public string Fone { get; set; }
        public string CNPJ { get; set; }
        public string Cep { get; set; }
        public byte? IndAtivo { get; set; }
        public virtual List<OrdemServico> OrdensServico { get; set; }
        public virtual FilialAnalista FilialAnalista { get; set; }
        public virtual OrcamentoISS OrcamentoISS { get; set; }
    }
}
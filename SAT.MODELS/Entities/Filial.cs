using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Filial
    {
        public int CodFilial { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFilial { get; set; }
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
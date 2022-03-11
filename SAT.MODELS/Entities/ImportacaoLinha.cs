using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class ImportacaoLinha
    {
        public List<ImportacaoColuna> ImportacaoColuna { get; set; }
        public bool? Erro { get; set; }
        public string Mensagem { get; set; }
    }
}
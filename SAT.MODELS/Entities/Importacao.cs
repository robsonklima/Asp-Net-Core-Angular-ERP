using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Importacao
    {
        public int Id { get; set; }
        public List<ImportacaoLinha> ImportacaoLinhas { get; set; }
    }
}
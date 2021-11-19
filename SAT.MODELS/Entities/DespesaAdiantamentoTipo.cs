using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaAdiantamentoTipo
    {
        [Key]
        public int CodDespesaAdiantamentoTipo { get; set; }
        public string NomeAdiantamentoTipo { get; set; }
    }
}
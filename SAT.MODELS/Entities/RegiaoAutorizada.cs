using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class RegiaoAutorizada
    {
        public int? CodRegiao { get; set; }
        [ForeignKey("CodRegiao")]
        public Regiao Regiao { get; set; }
        public int? CodAutorizada { get; set; }
        [ForeignKey("CodAutorizada")]
        public Autorizada Autorizada { get; set; }
        public int? CodFilial { get; set; }
        [ForeignKey("CodFilial")]
        public Filial Filial { get; set; }
        public int? CodCidade { get; set; }
        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }
        public int? PA { get; set; }
        public byte IndAtivo { get; set; }
        //public virtual OrdemServico OrdemServico { get; set; }

    }
}

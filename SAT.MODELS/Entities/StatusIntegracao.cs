using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class StatusIntegracao
    {
        [Key]
        public int CodStatusIntegracao { get; set; }
        public string NomeStatusIntegracao { get; set; }
    }
}

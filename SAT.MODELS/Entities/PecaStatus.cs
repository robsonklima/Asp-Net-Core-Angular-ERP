using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class PecaStatus
    {
        [Key]
        public int CodPecaStatus { get; set; }
        public string SiglaStatus { get; set; }
        public string NomeStatus { get; set; }
        public string MsgStatus { get; set; }
    }
}

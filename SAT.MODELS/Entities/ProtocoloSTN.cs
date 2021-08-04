using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class ProtocoloSTN
    {
        [Key]
        public int? CodProtocoloSTN { get; set; }
        public int CodRAT { get; set; }
        public int CodOS { get; set; }
        public int NumProtocolo { get; set; }
    }
}

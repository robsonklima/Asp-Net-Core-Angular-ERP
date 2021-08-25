using SAT.MODELS.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS
{
    [NotMapped]
    public class Indicador
    {
        public string Cliente { get; set; }
        public int QtdOS { get; set; }
        public int QtdOSFora { get; set; }
        public int QtdOSDentro { get; set; }
        public int QtdOSReincidentes { get; set; }
        public int QtdOSPendentes { get; set; }
    }
}

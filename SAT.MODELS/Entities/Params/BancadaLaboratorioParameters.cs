using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class BancadaLaboratorioParameters : QueryStringParameters
    {
        public int NumBancada { get; set; }
        public string CodUsuario { get; set; }
        public int NumBancadas { get; set; }
        public string CodUsuarios { get; set; }
    }
}

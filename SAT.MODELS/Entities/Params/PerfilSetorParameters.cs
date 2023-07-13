using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PerfilSetorParameters : QueryStringParameters
    {
        public int? CodPerfilSetor { get; set; }
        public int? CodPerfil { get; set; }
        public int? CodSetor { get; set; }
        public string CodSetores { get; set; }
    }
}

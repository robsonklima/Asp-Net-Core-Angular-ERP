using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PecasLaboratorioParameters : QueryStringParameters
    {
        public string CodPecas { get; set; }
        public string CodMagnus { get; set; }
        public int? CodChecklist { get; set; }

    }
}

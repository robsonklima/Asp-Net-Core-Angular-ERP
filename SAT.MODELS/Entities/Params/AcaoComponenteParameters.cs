using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class AcaoComponenteParameters : QueryStringParameters
    {
        public string CodECausa { get; set; }

        public string CodECausas { get; set; }

        public string CodEAcoes { get; set; }

        public string NomeCausas { get; set; }

        public string NomeAcoes { get; set; }


    }
}

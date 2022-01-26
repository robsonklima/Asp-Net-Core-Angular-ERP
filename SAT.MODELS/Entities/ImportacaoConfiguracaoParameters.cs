using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class ImportacaoConfiguracaoParameters : QueryStringParameters
	{
		public int? CodImportacaoTipo { get; set; }
		public string Titulo { get; set; }

	}
}

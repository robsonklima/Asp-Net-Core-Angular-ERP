using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class ImportacaoTipoParameters : QueryStringParameters
	{
		public int? CodImportacaoTipo { get; set; }
		public string NomeTipo { get; set; }
		public int? IndAtivo { get; set; }

	}
}

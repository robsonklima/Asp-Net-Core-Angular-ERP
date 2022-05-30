namespace SAT.MODELS.Entities
{
    public class ImportacaoConfiguracao
    {
		public int CodImportacaoConf { get; set; }
		public int CodImportacaoTipo { get; set; }
		public ImportacaoTipo ImportacaoTipo { get; set; }
		public string Titulo { get; set; }
		public string Propriedade { get; set; }
		public int? Largura { get; set; }
		public string TipoHeader { get; set; }
		public string Mascara { get; set; }
		public string Decimal { get; set; }
		public string Render { get; set; }
		public string Editor { get; set; }
	}
}

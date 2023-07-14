namespace SAT.MODELS.Entities
{
    public class DocumentoSistema
    {
        public int CodDocumentoSistema { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public string Categoria { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class Exportacao
    {
        public ExportacaoFormatoEnum FormatoArquivo { get; set; }
        public ExportacaoTipoEnum TipoArquivo { get; set; }
        public dynamic EntityParameters { get; set; }
        public Email Email { get; set; }
    }
}

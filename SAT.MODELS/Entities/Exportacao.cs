using System.ComponentModel.DataAnnotations;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class Exportacao
    {
        public Email Email { get; set; }
        public ExportacaoFormatoEnum FormatoArquivo { get; set; }
        public ExportacaoTipoEnum TipoArquivo { get; set; }
        public dynamic EntityParameters { get; set; }
    }
}

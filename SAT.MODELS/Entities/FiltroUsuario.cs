using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class FiltroUsuario
    {
        [Key]
        public int CodFiltroUsuario { get; set; }
        public string CodUsuario { get; set; }
        public string NomeFiltro { get; set; }
        public string ComponenteFiltro { get; set; }
        public string DadosJson { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaPeriodoTecnicoStatus
    {
        [Key]
        public int CodDespesaPeriodoTecnicoStatus { get; set; }
        public string NomeDespesaPeriodoTecnicoStatus { get; set; }
    }
}
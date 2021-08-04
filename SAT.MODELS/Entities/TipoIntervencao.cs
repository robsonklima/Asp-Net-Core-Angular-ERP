using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoIntervencao
    {
        [Key]
        public int? CodTipoIntervencao { get; set; }
        public string CodETipoIntervencao { get; set; }
        public string NomTipoIntervencao { get; set; }
        public byte? CalcPreventivaIntervenc { get; set; }
        public byte? VerificaReincidenciaInt { get; set; }
        public int? CodTraducao { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

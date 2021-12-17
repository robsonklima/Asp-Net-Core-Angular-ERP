using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DispBBCalcEquipamentoContratoParameters : QueryStringParameters
    {
        public int? CodDispBBRegiao { get; set; }
        public int? Criticidade { get; set; }
        public string AnoMes { get; set; }

    }
}
using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{    public class ORCheckListParameters : QueryStringParameters
    {
        public int? CodORCheckList { get; set; }
        public string Descricao { get; set; }
        public string CodUsuariosCad { get; set; }
        public DateTime DataHoraCadInicio { get; set; }
        public DateTime DataHoraCadFim { get; set; }
    }
}

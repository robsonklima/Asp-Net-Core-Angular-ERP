using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class CheckListPOSParameters : QueryStringParameters {
        public int CodCheckListPOS { get; set; }
        public string CodCheckListPOSItens { get; set; }
        public int? CodOS { get; set; }
        public int? CodRAT { get; set; }

    }
}
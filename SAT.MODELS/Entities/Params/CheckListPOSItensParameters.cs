using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params {
    public class CheckListPOSItensParameters : QueryStringParameters {

        public int CodCheckListPOSItens { get; set; }
        public int? CodCliente { get; set; }
        public string CodClientes { get; set; }
        public int? IndAtivo { get; set; }
        public int? IndPadrao { get; set; }


    }
}
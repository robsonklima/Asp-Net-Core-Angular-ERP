using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{    public class ItemXORCheckListParameters : QueryStringParameters
    {
        public int? CodItemChecklist { get; set; }
        public int? CodORItem { get; set; }
        public int? CodORCheckList { get; set; }
        public int? CodORCheckListItem { get; set; }
        public int? IndAtivo { get; set; }
        public string Nivel { get; set; }        
    }
}

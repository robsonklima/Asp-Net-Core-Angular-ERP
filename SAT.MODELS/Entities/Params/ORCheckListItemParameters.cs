using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{    public class ORCheckListItemParameters : QueryStringParameters
    {
        public int? CodORCheckListItem { get; set; }
        public string CodORCheckListItems { get; set; }
        public string Nivel { get; set; }
    }
}

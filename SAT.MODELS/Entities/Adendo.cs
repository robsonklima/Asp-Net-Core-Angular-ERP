using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Adendo
    {
        public int CodAdendo { get; set; }
        public List<AdendoItem> Itens { get; set; }
    }

    public class AdendoItem
    {
        public int CodAdendoItem { get; set; }
        public int CodAdendo { get; set; }
        public int CodEquipContrato { get; set; }
    }
}


namespace SAT.MODELS.Entities
{

    public class CheckListPOS
    {
        public int CodCheckListPOS { get; set; }
        public int CodOS { get; set; }
        public int CodRAT { get; set; }
        public int CodCheckListPOSItens { get; set; }
        public CheckListPOSItens CheckListPOSItens { get; set; }

    }
}


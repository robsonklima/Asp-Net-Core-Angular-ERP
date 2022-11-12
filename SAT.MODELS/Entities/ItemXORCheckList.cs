namespace SAT.MODELS.Entities{
    public class ItemXORCheckList 
    { 
        public int? CodItemChecklist { get; set; }
        public int? CodORItem { get; set; }
        public ORItem ORItem { get; set; }
        public int? CodORCheckList { get; set; }
        public ORCheckList ORCheckList { get; set; }
        public int? CodORCheckListItem { get; set; }
        public ORCheckListItem ORCheckListItem { get; set; }
        public int? IndAtivo { get; set; }
        public string Nivel { get; set; }
    }
}
namespace SAT.MODELS.Entities
{
    public class ORCheckListItem
    {
        public int CodORCheckListItem { get; set; }
        public string CodMagnus { get; set; }
        public string Descricao { get; set; }
        public string Nivel { get; set; }
        public string Acao { get; set; }
        public string Parametro { get; set; }
        public string Realizacao { get; set; }
        public string PnMei { get; set; }
        public int? CodORCheckList { get; set; }
        public int? PassoObrigatorio { get; set; }
    }
}
namespace SAT.MODELS.Entities
{
    public class CheckListPOSItens
    {
        public int CodCheckListPOSItens { get; set; }
        public int CodCliente { get; set; }
        public string Descricao { get; set; }
        public byte IndAtivo { get; set; }
        public Cliente Cliente { get; set; }
    }
}
namespace SAT.MODELS.Entities
{
    public class OrcamentoAprovacao
    {
        public int CodOrc { get; set; }
        public string Motivo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Departamento { get; set; }
        public string Telefone { get; set; }
        public string Ramal { get; set; }
        public bool IsAprovado { get; set; }
    }
}
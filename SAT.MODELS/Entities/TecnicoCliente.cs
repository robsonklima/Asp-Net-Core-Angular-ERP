namespace SAT.MODELS.Entities
{
    public class TecnicoCliente
    {
        public int CodTecnicoCliente { get; set; }
        public int CodTecnico { get; set; }
        public int CodCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
namespace SAT.MODELS.Entities
{
    public class NavegacaoConfiguracaoTipo
    {
        public int CodNavegacaoConfTipo { get; set; }
        public string Descricao { get; set; }
        public int PermiteConsultar { get; set; }	
        public int PermiteCriar { get; set; }	
        public int PermiteEditar { get; set; }	
        public int PermiteExcluir { get; set; }	
        public int IndAtivo { get; set; }		
    }
}

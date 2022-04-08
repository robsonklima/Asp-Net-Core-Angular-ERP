using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class UsuarioParameters : QueryStringParameters
    {
        public string CodUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public int? CodFilial { get; set; }
        public int? CodPerfil { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodPontoPeriodo { get; set; }
        public string PAS { get; set; }
        public string CodTecnicos { get; set; }
        public int? IndFerias { get; set; }
    }
}

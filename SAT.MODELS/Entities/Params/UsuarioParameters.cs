using System;
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
        public string CodPerfis { get; set; }
        public string CodCargos { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodPontoPeriodo { get; set; }
        public string PAS { get; set; }
        public string CodTecnicos { get; set; }
        public string CodFiliais { get; set; }
        public int? IndFerias { get; set; }
        public int? IndPonto { get; set; }
        public string CodPerfisNotIn { get; set; }
        public DateTime? UltimoAcessoInicio { get; set; }
        public DateTime? UltimoAcessoFim { get; set; }
    }
}

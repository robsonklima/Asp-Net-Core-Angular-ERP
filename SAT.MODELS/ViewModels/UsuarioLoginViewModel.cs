using SAT.MODELS.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SAT.MODELS.ViewModels
{
    public class UsuarioLoginViewModel
    {
        public Usuario Usuario { get; set; }
        public List<Navegacao> Navegacoes { get; set; }
        public string Token { get; set; }
    }
}

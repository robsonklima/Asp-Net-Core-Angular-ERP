using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services
{
    public class UsuarioLoginService : IUsuarioLoginService
    {
        private readonly IUsuarioLoginRepository _UsuarioLoginRepo;

        public UsuarioLoginService(
            IUsuarioLoginRepository UsuarioLoginRepo
        )
        {
            _UsuarioLoginRepo = UsuarioLoginRepo;
        }

        public ListViewModel ObterPorParametros(UsuarioLoginParameters parameters)
        {
            var logins = _UsuarioLoginRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = logins,
                TotalCount = logins.TotalCount,
                CurrentPage = logins.CurrentPage,
                PageSize = logins.PageSize,
                TotalPages = logins.TotalPages,
                HasNext = logins.HasNext,
                HasPrevious = logins.HasPrevious
            };

            return lista;
        }

        public UsuarioLogin Criar(UsuarioLogin login)
        {
            return _UsuarioLoginRepo.Criar(login);
        }
    }
}

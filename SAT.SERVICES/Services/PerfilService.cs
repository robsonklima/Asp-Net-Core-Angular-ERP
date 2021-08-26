using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepo;

        public PerfilService(IPerfilRepository perfilRepo)
        {
            _perfilRepo = perfilRepo;
        }

        public ListViewModel ObterPorParametros(PerfilParameters parameters)
        {
            var perfis = _perfilRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = perfis,
                TotalCount = perfis.TotalCount,
                CurrentPage = perfis.CurrentPage,
                PageSize = perfis.PageSize,
                TotalPages = perfis.TotalPages,
                HasNext = perfis.HasNext,
                HasPrevious = perfis.HasPrevious
            };

            return lista;
        }

        public Perfil Criar(Perfil Perfil)
        {
            _perfilRepo.Criar(Perfil);
            return Perfil;
        }

        public void Deletar(int codigo)
        {
            _perfilRepo.Deletar(codigo);
        }

        public void Atualizar(Perfil perfil)
        {
            _perfilRepo.Atualizar(perfil);
        }

        public Perfil ObterPorCodigo(int codigo)
        {
            return _perfilRepo.ObterPorCodigo(codigo);
        }
    }
}

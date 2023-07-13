using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class NavegacaoService : INavegacaoService
    {
        private readonly INavegacaoRepository _navegacaoRepo;

        public NavegacaoService(INavegacaoRepository navegacaoRepo, ISequenciaRepository seqRepo)
        {
            _navegacaoRepo = navegacaoRepo;
        }

        public ListViewModel ObterPorParametros(NavegacaoParameters parameters)
        {
            var perfis = _navegacaoRepo.ObterPorParametros(parameters);

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

        public Navegacao Criar(Navegacao navegacao)
        {
            _navegacaoRepo.Criar(navegacao);
            return navegacao;
        }

        public void Deletar(int codigo)
        {
            _navegacaoRepo.Deletar(codigo);
        }

        public void Atualizar(Navegacao navegacao)
        {
            _navegacaoRepo.Atualizar(navegacao);
        }

        public Navegacao ObterPorCodigo(int codigo)
        {
            return _navegacaoRepo.ObterPorCodigo(codigo);
        }
    }
}

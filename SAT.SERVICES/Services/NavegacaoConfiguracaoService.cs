using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class NavegacaoConfiguracaoService : INavegacaoConfiguracaoService
    {
        private readonly INavegacaoConfiguracaoRepository _navegacaoConfiguracaoRepo;

        public NavegacaoConfiguracaoService(INavegacaoConfiguracaoRepository navegacaoConfiguracaoRepo)
        {
            _navegacaoConfiguracaoRepo = navegacaoConfiguracaoRepo;
        }

        public ListViewModel ObterPorParametros(NavegacaoConfiguracaoParameters parameters)
        {
            var perfis = _navegacaoConfiguracaoRepo.ObterPorParametros(parameters);

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

        public NavegacaoConfiguracao Criar(NavegacaoConfiguracao navegacaoConfiguracao)
        {
            _navegacaoConfiguracaoRepo.Criar(navegacaoConfiguracao);
            return navegacaoConfiguracao;
        }

        public void Deletar(int codigo)
        {
            _navegacaoConfiguracaoRepo.Deletar(codigo);
        }

        public void Atualizar(NavegacaoConfiguracao navegacaoConfiguracao)
        {
            _navegacaoConfiguracaoRepo.Atualizar(navegacaoConfiguracao);
        }

        public NavegacaoConfiguracao ObterPorCodigo(int codigo)
        {
            return _navegacaoConfiguracaoRepo.ObterPorCodigo(codigo);
        }
    }
}

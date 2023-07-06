using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class NavegacaoConfiguracaoTipoService : INavegacaoConfiguracaoTipoService
    {
        private readonly INavegacaoConfiguracaoTipoRepository _navegacaoConfiguracaoTipoRepo;

        public NavegacaoConfiguracaoTipoService(INavegacaoConfiguracaoTipoRepository navegacaoConfiguracaoTipoRepo)
        {
            _navegacaoConfiguracaoTipoRepo = navegacaoConfiguracaoTipoRepo;
        }

        public ListViewModel ObterPorParametros(NavegacaoConfiguracaoTipoParameters parameters)
        {
            var perfis = _navegacaoConfiguracaoTipoRepo.ObterPorParametros(parameters);

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

        public NavegacaoConfiguracaoTipo Criar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo)
        {
            _navegacaoConfiguracaoTipoRepo.Criar(navegacaoConfiguracaoTipo);
            return navegacaoConfiguracaoTipo;
        }

        public void Deletar(int codigo)
        {
            _navegacaoConfiguracaoTipoRepo.Deletar(codigo);
        }

        public void Atualizar(NavegacaoConfiguracaoTipo navegacaoConfiguracaoTipo)
        {
            _navegacaoConfiguracaoTipoRepo.Atualizar(navegacaoConfiguracaoTipo);
        }

        public NavegacaoConfiguracaoTipo ObterPorCodigo(int codigo)
        {
            return _navegacaoConfiguracaoTipoRepo.ObterPorCodigo(codigo);
        }
    }
}

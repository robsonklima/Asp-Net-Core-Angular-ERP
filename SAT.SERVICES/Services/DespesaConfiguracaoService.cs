using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaConfiguracaoService : IDespesaConfiguracaoService
    {
        private readonly IDespesaConfiguracaoRepository _despesaRepo;

        public DespesaConfiguracaoService(IDespesaConfiguracaoRepository despesaRepo)
        {
            _despesaRepo = despesaRepo;
        }

        public ListViewModel ObterPorParametros(DespesaConfiguracaoParameters parameters)
        {
            var despesas =
                _despesaRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = despesas,
                TotalCount = despesas.TotalCount,
                CurrentPage = despesas.CurrentPage,
                PageSize = despesas.PageSize,
                TotalPages = despesas.TotalPages,
                HasNext = despesas.HasNext,
                HasPrevious = despesas.HasPrevious
            };
        }

        public void Atualizar(DespesaConfiguracao configuracao)
        {
            _despesaRepo.Atualizar(configuracao);
        }

        public DespesaConfiguracao Criar(DespesaConfiguracao configuracao)
        {
            _despesaRepo.Criar(configuracao);

            return configuracao;
        }

        public DespesaConfiguracao ObterPorCodigo(int codigo)
        {
            return _despesaRepo.ObterPorCodigo(codigo);
        }
    }
}
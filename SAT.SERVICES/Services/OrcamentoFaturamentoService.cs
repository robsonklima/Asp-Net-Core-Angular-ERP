using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoFaturamentoService : IOrcamentoFaturamentoService
    {
        private readonly IOrcamentoFaturamentoRepository _orcamentoFaturamentoRepo;

        public OrcamentoFaturamentoService(IOrcamentoFaturamentoRepository orcamentoFaturamentoRepo)
        {
            _orcamentoFaturamentoRepo = orcamentoFaturamentoRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoFaturamentoParameters parameters)
        {
            var faturamentos = _orcamentoFaturamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = faturamentos,
                TotalCount = faturamentos.TotalCount,
                CurrentPage = faturamentos.CurrentPage,
                PageSize = faturamentos.PageSize,
                TotalPages = faturamentos.TotalPages,
                HasNext = faturamentos.HasNext,
                HasPrevious = faturamentos.HasPrevious
            };

            return lista;
        }

        public OrcamentoFaturamento Criar(OrcamentoFaturamento orcamento)
        {
            _orcamentoFaturamentoRepo.Criar(orcamento);
            return orcamento;
        }

        public void Deletar(int codigo)
        {
            _orcamentoFaturamentoRepo.Deletar(codigo);
        }

        public OrcamentoFaturamento Atualizar(OrcamentoFaturamento orcamento)
        {
            _orcamentoFaturamentoRepo.Atualizar(orcamento);
            return orcamento;
        }

        public OrcamentoFaturamento ObterPorCodigo(int codigo)
        {
            return _orcamentoFaturamentoRepo.ObterPorCodigo(codigo);
        }
    }
}

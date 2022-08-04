using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentosFaturamentoService : IOrcamentosFaturamentoService
    {
        private readonly IOrcamentosFaturamentoRepository _orcamentoFaturamentoRepo;

        public OrcamentosFaturamentoService(IOrcamentosFaturamentoRepository orcamentoFaturamentoRepo)
        {
            _orcamentoFaturamentoRepo = orcamentoFaturamentoRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentosFaturamentoParameters parameters)
        {
            var orcamentos = _orcamentoFaturamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = orcamentos,
                TotalCount = orcamentos.TotalCount,
                CurrentPage = orcamentos.CurrentPage,
                PageSize = orcamentos.PageSize,
                TotalPages = orcamentos.TotalPages,
                HasNext = orcamentos.HasNext,
                HasPrevious = orcamentos.HasPrevious
            };

            return lista;
        }

        public OrcamentosFaturamento Criar(OrcamentosFaturamento orcamento)
        {
            _orcamentoFaturamentoRepo.Criar(orcamento);
            return orcamento;
        }

        public void Deletar(int codigo)
        {
            _orcamentoFaturamentoRepo.Deletar(codigo);
        }

        public OrcamentosFaturamento Atualizar(OrcamentosFaturamento orcamento)
        {
            _orcamentoFaturamentoRepo.Atualizar(orcamento);
            return orcamento;
        }

        public OrcamentosFaturamento ObterPorCodigo(int codigo)
        {
            return _orcamentoFaturamentoRepo.ObterPorCodigo(codigo);
        }
    }
}

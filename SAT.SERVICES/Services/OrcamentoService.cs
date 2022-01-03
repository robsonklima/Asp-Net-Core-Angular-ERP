using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoService : IOrcamentoService
    {
        private readonly IOrcamentoRepository _orcamentoRepo;

        public OrcamentoService(IOrcamentoRepository orcamentoRepo)
        {
            _orcamentoRepo = orcamentoRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoParameters parameters)
        {
            var orcamentos = _orcamentoRepo.ObterPorParametros(parameters);

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

        public Orcamento Criar(Orcamento orcamento)
        {
            _orcamentoRepo.Criar(orcamento);
            return orcamento;
        }

        public void Deletar(int codigo)
        {
            _orcamentoRepo.Deletar(codigo);
        }

        public void Atualizar(Orcamento orcamento)
        {
            _orcamentoRepo.Atualizar(orcamento);
        }

        public Orcamento ObterPorCodigo(int codigo)
        {
            return _orcamentoRepo.ObterPorCodigo(codigo);
        }
    }
}

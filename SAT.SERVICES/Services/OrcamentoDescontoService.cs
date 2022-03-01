using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoDescontoService : IOrcamentoDescontoService
    {
        private readonly IOrcamentoDescontoRepository _orcamentoDescRepo;

        public OrcamentoDescontoService(IOrcamentoDescontoRepository orcamentoDescRepo)
        {
            _orcamentoDescRepo = orcamentoDescRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoDescontoParameters parameters)
        {
            var orcamentos = _orcamentoDescRepo.ObterPorParametros(parameters);

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

        public OrcamentoDesconto Criar(OrcamentoDesconto orcamentoDesc)
        {
            _orcamentoDescRepo.Criar(orcamentoDesc);
            return orcamentoDesc;
        }

        public void Deletar(int codigo)
        {
            _orcamentoDescRepo.Deletar(codigo);
        }

        public OrcamentoDesconto Atualizar(OrcamentoDesconto orcamentoDesc)
        {
            _orcamentoDescRepo.Atualizar(orcamentoDesc);
            return orcamentoDesc;
        }

        public OrcamentoDesconto ObterPorCodigo(int codigo)
        {
            return _orcamentoDescRepo.ObterPorCodigo(codigo);
        }
    }
}

using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrcamentoMaoDeObraService : IOrcamentoMaoDeObraService
    {
        private readonly IOrcamentoMaoDeObraRepository _orcamentoMaoDeObraService;

        public OrcamentoMaoDeObraService(IOrcamentoMaoDeObraRepository orcamentoMatRepo)
        {
            _orcamentoMaoDeObraService = orcamentoMatRepo;
        }

        public ListViewModel ObterPorParametros(OrcamentoMaoDeObraParameters parameters)
        {
            var orcamentos = _orcamentoMaoDeObraService.ObterPorParametros(parameters);

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

        public OrcamentoMaoDeObra Criar(OrcamentoMaoDeObra orcamentoMaoDeObra)
        {
            _orcamentoMaoDeObraService.Criar(orcamentoMaoDeObra);
            return orcamentoMaoDeObra;
        }

        public void Deletar(int codigo)
        {
            _orcamentoMaoDeObraService.Deletar(codigo);
        }

        public OrcamentoMaoDeObra Atualizar(OrcamentoMaoDeObra orcamentoMaoDeObra)
        {
            _orcamentoMaoDeObraService.Atualizar(orcamentoMaoDeObra);
            return orcamentoMaoDeObra;
        }

        public OrcamentoMaoDeObra ObterPorCodigo(int codigo)
        {
            return _orcamentoMaoDeObraService.ObterPorCodigo(codigo);
        }
    }
}

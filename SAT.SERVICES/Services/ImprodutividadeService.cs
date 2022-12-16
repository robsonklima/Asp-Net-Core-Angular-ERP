using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ImprodutividadeService : IImprodutividadeService
    {
        private readonly IImprodutividadeRepository _improdutividadeRepo;

        public ImprodutividadeService(
            IImprodutividadeRepository improdutividadeRepo
        )
        {
            _improdutividadeRepo = improdutividadeRepo;
        }
        public Improdutividade ObterPorCodigo(int codImprodutividade)
        {
            return _improdutividadeRepo.ObterPorCodigo(codImprodutividade);
        }

        public ListViewModel ObterPorParametros(ImprodutividadeParameters parameters)
        {
            var improdutividades = _improdutividadeRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = improdutividades,
                TotalCount = improdutividades.TotalCount,
                CurrentPage = improdutividades.CurrentPage,
                PageSize = improdutividades.PageSize,
                TotalPages = improdutividades.TotalPages,
                HasNext = improdutividades.HasNext,
                HasPrevious = improdutividades.HasPrevious
            };

            return lista;
        }

    }
}

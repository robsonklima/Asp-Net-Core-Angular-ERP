using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CausaImprodutividadeService : ICausaImprodutividadeService
    {
        private readonly ICausaImprodutividadeRepository _causaImprodutividadeRepo;

        public CausaImprodutividadeService(
            ICausaImprodutividadeRepository causaImprodutividadeRepo
        )
        {
            _causaImprodutividadeRepo = causaImprodutividadeRepo;
        }

        public void Atualizar(CausaImprodutividade causaImprodutividade)
        {
            _causaImprodutividadeRepo.Atualizar(causaImprodutividade);
        }

        public void Criar(CausaImprodutividade causaImprodutividade)
        {
            _causaImprodutividadeRepo.Criar(causaImprodutividade);
        }

        public void Deletar(int codCausaImprodutividade)
        {
            _causaImprodutividadeRepo.Deletar(codCausaImprodutividade);
        }

        public CausaImprodutividade ObterPorCodigo(int codCausaImprodutividade)
        {
            return _causaImprodutividadeRepo.ObterPorCodigo(codCausaImprodutividade);
        }

        public ListViewModel ObterPorParametros(CausaImprodutividadeParameters parameters)
        {
            var causaImprodutividades = _causaImprodutividadeRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = causaImprodutividades,
                TotalCount = causaImprodutividades.TotalCount,
                CurrentPage = causaImprodutividades.CurrentPage,
                PageSize = causaImprodutividades.PageSize,
                TotalPages = causaImprodutividades.TotalPages,
                HasNext = causaImprodutividades.HasNext,
                HasPrevious = causaImprodutividades.HasPrevious
            };

            return lista;
        }

    }
}

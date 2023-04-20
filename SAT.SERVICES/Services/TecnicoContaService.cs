using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class TecnicoContaService : ITecnicoContaService
    {
        private readonly ITecnicoContaRepository _tecnicoContaRepo;

        public TecnicoContaService(
            ITecnicoContaRepository tecnicoContaRepo
        )
        {
            _tecnicoContaRepo = tecnicoContaRepo;
        }

        public ListViewModel ObterPorParametros(TecnicoContaParameters parameters)
        {
            PagedList<TecnicoConta> TecnicoContas = _tecnicoContaRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = TecnicoContas,
                TotalCount = TecnicoContas.TotalCount,
                CurrentPage = TecnicoContas.CurrentPage,
                PageSize = TecnicoContas.PageSize,
                TotalPages = TecnicoContas.TotalPages,
                HasNext = TecnicoContas.HasNext,
                HasPrevious = TecnicoContas.HasPrevious
            };
        }

        public TecnicoConta Criar(TecnicoConta TecnicoConta)
        {
            return _tecnicoContaRepo.Criar(TecnicoConta);
        }

        public void Deletar(int codigo)
        {
            _tecnicoContaRepo.Deletar(codigo);
        }

        public void Atualizar(TecnicoConta TecnicoConta)
        {
            _tecnicoContaRepo.Atualizar(TecnicoConta);
        }

        public TecnicoConta ObterPorCodigo(int codigo)
        {
            return _tecnicoContaRepo.ObterPorCodigo(codigo);
        }
    }
}
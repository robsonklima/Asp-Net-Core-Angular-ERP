using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RegiaoAutorizadaService : IRegiaoAutorizadaService
    {
        private readonly IRegiaoAutorizadaRepository _regiaoRepo;

        public RegiaoAutorizadaService(IRegiaoAutorizadaRepository regiaoRepo)
        {
            _regiaoRepo = regiaoRepo;
        }

        public ListViewModel ObterPorParametros(RegiaoAutorizadaParameters parameters)
        {
            var regioes = _regiaoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = regioes,
                TotalCount = regioes.TotalCount,
                CurrentPage = regioes.CurrentPage,
                PageSize = regioes.PageSize,
                TotalPages = regioes.TotalPages,
                HasNext = regioes.HasNext,
                HasPrevious = regioes.HasPrevious
            };

            return lista;
        }

        public RegiaoAutorizada Criar(RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoRepo.Criar(regiaoAutorizada);
            return regiaoAutorizada;
        }

        public void Atualizar(RegiaoAutorizada regiaoAutorizada)
        {
            _regiaoRepo.Atualizar(regiaoAutorizada);
        }

        public RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial)
        {
            return _regiaoRepo.ObterPorCodigo(codRegiao, codAutorizada, codFilial);
        }
    }
}

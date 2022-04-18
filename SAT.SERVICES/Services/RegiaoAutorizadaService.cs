using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RegiaoAutorizadaService : IRegiaoAutorizadaService
    {
        private readonly IRegiaoAutorizadaRepository _regiaoAutorizadaRepo;

        public RegiaoAutorizadaService(IRegiaoAutorizadaRepository regiaoAutorizadaRepo)
        {
            _regiaoAutorizadaRepo = regiaoAutorizadaRepo;
        }

        public ListViewModel ObterPorParametros(RegiaoAutorizadaParameters parameters)
        {
            var regioes = _regiaoAutorizadaRepo.ObterPorParametros(parameters);

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
            _regiaoAutorizadaRepo.Criar(regiaoAutorizada);
            return regiaoAutorizada;
        }

        public RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial)
        {
            return _regiaoAutorizadaRepo.ObterPorCodigo(codRegiao, codAutorizada, codFilial);
        }

        public void Deletar(int codRegiao, int codAutorizada, int codFilial)
        {
         _regiaoAutorizadaRepo.Deletar(codRegiao, codAutorizada, codFilial);
        }
    }
}

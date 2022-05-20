using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DispBBBloqueioOSService : IDispBBBloqueioOSService
    {
        private readonly IDispBBBloqueioOSRepository _dispBBBloqueioOSRepo;

        public DispBBBloqueioOSService(IDispBBBloqueioOSRepository dispBBBloqueioOSRepo)
        {
            _dispBBBloqueioOSRepo = dispBBBloqueioOSRepo;
        }

        public void Atualizar(DispBBBloqueioOS dispBBBloqueioOS)
        {
            _dispBBBloqueioOSRepo.Atualizar(dispBBBloqueioOS);
        }

        public DispBBBloqueioOS Criar(DispBBBloqueioOS dispBBBloqueioOS)
        {
            _dispBBBloqueioOSRepo.Criar(dispBBBloqueioOS);

            return dispBBBloqueioOS;
        }

        public void Deletar(int codigo)
        {
            _dispBBBloqueioOSRepo.Deletar(codigo);
        }

        public DispBBBloqueioOS ObterPorCodigo(int codigo)
        {
            return _dispBBBloqueioOSRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DispBBBloqueioOSParameters parameters)
        {
            var dispBBBloqueioOSs = _dispBBBloqueioOSRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = dispBBBloqueioOSs,
                TotalCount = dispBBBloqueioOSs.TotalCount,
                CurrentPage = dispBBBloqueioOSs.CurrentPage,
                PageSize = dispBBBloqueioOSs.PageSize,
                TotalPages = dispBBBloqueioOSs.TotalPages,
                HasNext = dispBBBloqueioOSs.HasNext,
                HasPrevious = dispBBBloqueioOSs.HasPrevious
            };

            return lista;
        }
    }
}
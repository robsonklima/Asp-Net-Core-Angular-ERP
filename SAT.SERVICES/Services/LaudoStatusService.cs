using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LaudoStatusService : ILaudoStatusService
    {
        private readonly ILaudoStatusRepository _laudoStatusRepo;

        public LaudoStatusService(ILaudoStatusRepository laudoStatusRepo)
        {
            _laudoStatusRepo = laudoStatusRepo;
        }

        public LaudoStatus ObterPorCodigo(int codigo) =>
            this._laudoStatusRepo.ObterPorCodigo(codigo);

        public ListViewModel ObterPorParametros(LaudoStatusParameters parameters)
        {
            var laudos = _laudoStatusRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = laudos,
                TotalCount = laudos.TotalCount,
                CurrentPage = laudos.CurrentPage,
                PageSize = laudos.PageSize,
                TotalPages = laudos.TotalPages,
                HasNext = laudos.HasNext,
                HasPrevious = laudos.HasPrevious
            };

            return lista;
        }
    }
}
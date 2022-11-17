using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class LaudoService : ILaudoService
    {
        private readonly ILaudoRepository _laudoRepo;

        public LaudoService(ILaudoRepository laudoRepo)
        {
            _laudoRepo = laudoRepo;
        }

        public void Atualizar(Laudo laudo)
        {
            _laudoRepo.Atualizar(laudo);
        }
        public Laudo ObterPorCodigo(int codigo) =>
            this._laudoRepo.ObterPorCodigo(codigo);

        public ListViewModel ObterPorParametros(LaudoParameters parameters)
        {
            var laudos = _laudoRepo.ObterPorParametros(parameters);

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
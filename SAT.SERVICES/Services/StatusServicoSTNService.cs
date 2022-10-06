using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class StatusServicoSTNService : IStatusServicoSTNService
    {
        private readonly IStatusServicoSTNRepository _statusServicoSTNRepo;

        public StatusServicoSTNService(IStatusServicoSTNRepository StatusServicoSTNRepo)
        {
            _statusServicoSTNRepo = StatusServicoSTNRepo;
        }

        public ListViewModel ObterPorParametros(StatusServicoSTNParameters parameters)
        {
            var StatusServicoSTNs = _statusServicoSTNRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = StatusServicoSTNs,
                TotalCount = StatusServicoSTNs.TotalCount,
                CurrentPage = StatusServicoSTNs.CurrentPage,
                PageSize = StatusServicoSTNs.PageSize,
                TotalPages = StatusServicoSTNs.TotalPages,
                HasNext = StatusServicoSTNs.HasNext,
                HasPrevious = StatusServicoSTNs.HasPrevious
            };

            return lista;
        }

        public StatusServicoSTN Criar(StatusServicoSTN StatusServicoSTN)
        {
            return _statusServicoSTNRepo.Criar(StatusServicoSTN);
        }

        public void Deletar(int codigo)
        {
            _statusServicoSTNRepo.Deletar(codigo);
        }

        public void Atualizar(StatusServicoSTN StatusServicoSTN)
        {
            _statusServicoSTNRepo.Atualizar(StatusServicoSTN);
        }

        public StatusServicoSTN ObterPorCodigo(int codigo)
        {
            return _statusServicoSTNRepo.ObterPorCodigo(codigo);
        }
    }
}

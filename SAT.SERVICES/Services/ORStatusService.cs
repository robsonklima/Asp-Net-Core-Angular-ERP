using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ORStatusService : IORStatusService
    {
        private readonly IORStatusRepository _ORStatusRepo;

        public ORStatusService(IORStatusRepository ORStatusRepo)
        {
            _ORStatusRepo = ORStatusRepo;
        }

        public ListViewModel ObterPorParametros(ORStatusParameters parameters)
        {
            var status = _ORStatusRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = status,
                TotalCount = status.TotalCount,
                CurrentPage = status.CurrentPage,
                PageSize = status.PageSize,
                TotalPages = status.TotalPages,
                HasNext = status.HasNext,
                HasPrevious = status.HasPrevious
            };

            return lista;
        }

        public ORStatus Criar(ORStatus status)
        {
            _ORStatusRepo.Criar(status);

            return status;
        }

        public void Deletar(int codigo)
        {
            _ORStatusRepo.Deletar(codigo);
        }

        public void Atualizar(ORStatus status)
        {
            _ORStatusRepo.Atualizar(status);
        }

        public ORStatus ObterPorCodigo(int codigo)
        {
            return _ORStatusRepo.ObterPorCodigo(codigo);
        }
    }
}

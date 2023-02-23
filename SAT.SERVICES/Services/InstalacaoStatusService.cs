using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class InstalacaoStatusService : IInstalacaoStatusService
    {
        private readonly IInstalacaoStatusRepository _instalStatusRepo;

        public InstalacaoStatusService(IInstalacaoStatusRepository instalStatusRepo)
        {
            _instalStatusRepo = instalStatusRepo;
        }

        public ListViewModel ObterPorParametros(InstalacaoStatusParameters parameters)
        {
            var instalStatuss = _instalStatusRepo.ObterPorParametros(parameters);

            return new ListViewModel
            {
                Items = instalStatuss,
                TotalCount = instalStatuss.TotalCount,
                CurrentPage = instalStatuss.CurrentPage,
                PageSize = instalStatuss.PageSize,
                TotalPages = instalStatuss.TotalPages,
                HasNext = instalStatuss.HasNext,
                HasPrevious = instalStatuss.HasPrevious
            };
        }

        public InstalacaoStatus Criar(InstalacaoStatus instalStatus)
        {
            _instalStatusRepo.Criar(instalStatus);
            return instalStatus;
        }

        public void Deletar(int codigo)
        {
            _instalStatusRepo.Deletar(codigo);
        }

        public void Atualizar(InstalacaoStatus instalStatus)
        {
            _instalStatusRepo.Atualizar(instalStatus);
        }

        public InstalacaoStatus ObterPorCodigo(int codigo)
        {
            return _instalStatusRepo.ObterPorCodigo(codigo);
        }
    }
}

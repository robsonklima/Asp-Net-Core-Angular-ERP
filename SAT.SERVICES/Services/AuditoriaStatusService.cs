using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaStatusService : IAuditoriaStatusService
    {
        private readonly IAuditoriaStatusRepository _auditoriaStatusRepo;
        public AuditoriaStatusService( IAuditoriaStatusRepository auditoriaStatusRepo ) 
        {
            _auditoriaStatusRepo = auditoriaStatusRepo;
        }

        public void Atualizar(AuditoriaStatus auditoriaStatus)
        {
            _auditoriaStatusRepo.Atualizar(auditoriaStatus);
        }

        public void Criar(AuditoriaStatus auditoriaStatus)
        {
            _auditoriaStatusRepo.Criar(auditoriaStatus);
        }

        public void Deletar(int codigoAuditoriaStatus)
        {
            _auditoriaStatusRepo.Deletar(codigoAuditoriaStatus);
        }

        public AuditoriaStatus ObterPorCodigo(int codAuditoriaStatus)
        {
            return _auditoriaStatusRepo.ObterPorCodigo(codAuditoriaStatus);
        }

        public ListViewModel ObterPorParametros(AuditoriaStatusParameters parameters)
        {
            var auditoriasStatus = _auditoriaStatusRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = auditoriasStatus,
                TotalCount = auditoriasStatus.TotalCount,
                CurrentPage = auditoriasStatus.CurrentPage,
                PageSize = auditoriasStatus.PageSize,
                TotalPages = auditoriasStatus.TotalPages,
                HasNext = auditoriasStatus.HasNext,
                HasPrevious = auditoriasStatus.HasPrevious
            };

            return lista;
        }
    }
}

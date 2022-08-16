using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IAuditoriaRepository _auditoriaRepo;
        public AuditoriaService( IAuditoriaRepository auditoriaRepo ) 
        {
            _auditoriaRepo = auditoriaRepo;
        }

        public void Atualizar(Auditoria auditoria)
        {
            _auditoriaRepo.Atualizar(auditoria);
        }

        public void Criar(Auditoria auditoria)
        {
            _auditoriaRepo.Criar(auditoria);
        }

        public void Deletar(int codigoAuditoria)
        {
            _auditoriaRepo.Deletar(codigoAuditoria);
        }

        public Auditoria ObterPorCodigo(int codAuditoria)
        {
            return _auditoriaRepo.ObterPorCodigo(codAuditoria);
        }

        public ListViewModel ObterPorParametros(AuditoriaParameters parameters)
        {
            var auditorias = _auditoriaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = auditorias,
                TotalCount = auditorias.TotalCount,
                CurrentPage = auditorias.CurrentPage,
                PageSize = auditorias.PageSize,
                TotalPages = auditorias.TotalPages,
                HasNext = auditorias.HasNext,
                HasPrevious = auditorias.HasPrevious
            };

            return lista;
        }
    }
}

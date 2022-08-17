using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class AuditoriaVeiculoTanqueService : IAuditoriaVeiculoTanqueService
    {
        private IAuditoriaVeiculoTanqueRepository _auditoriaVeiculoTanqueRepo { get; }

        public AuditoriaVeiculoTanqueService(IAuditoriaVeiculoTanqueRepository auditoriaVeiculoTanqueRepo)
        {
            _auditoriaVeiculoTanqueRepo = auditoriaVeiculoTanqueRepo;
        }

        public ListViewModel ObterPorParametros(AuditoriaVeiculoTanqueParameters parameters)
        {
            var auditoriasVeiculosTanques = _auditoriaVeiculoTanqueRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = auditoriasVeiculosTanques,
                TotalCount = auditoriasVeiculosTanques.TotalCount,
                CurrentPage = auditoriasVeiculosTanques.CurrentPage,
                PageSize = auditoriasVeiculosTanques.PageSize,
                TotalPages = auditoriasVeiculosTanques.TotalPages,
                HasNext = auditoriasVeiculosTanques.HasNext,
                HasPrevious = auditoriasVeiculosTanques.HasPrevious
            };

            return lista;
        }

        public AuditoriaVeiculoTanque Criar(AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueRepo.Criar(auditoriaVeiculoTanque);

            return auditoriaVeiculoTanque;
        }

        public void Deletar(int codigo)
        {
            _auditoriaVeiculoTanqueRepo.Deletar(codigo);
        }

        public void Atualizar(AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueRepo.Atualizar(auditoriaVeiculoTanque);
        }

        public AuditoriaVeiculoTanque ObterPorCodigo(int codigo)
        {
            return _auditoriaVeiculoTanqueRepo.ObterPorCodigo(codigo);
        }
    }
}

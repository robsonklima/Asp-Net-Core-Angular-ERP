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

        // public ListViewModel ObterPorParametros(AuditoriaVeiculoParameters parameters)
        // {
        //     var auditoriaVeiculos = _auditoriaVeiculoRepo.ObterPorParametros(parameters);

        //     var lista = new ListViewModel
        //     {
        //         Items = causas,
        //         TotalCount = causas.TotalCount,
        //         CurrentPage = causas.CurrentPage,
        //         PageSize = causas.PageSize,
        //         TotalPages = causas.TotalPages,
        //         HasNext = causas.HasNext,
        //         HasPrevious = causas.HasPrevious
        //     };

        //     return lista;
        // }

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

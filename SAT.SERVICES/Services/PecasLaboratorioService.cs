using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PecasLaboratorioService : IPecasLaboratorioService
    {
        private readonly IPecasLaboratorioRepository _pecasLaboratorioRepo;
        private readonly ISequenciaRepository _sequenciaRepo;

        public PecasLaboratorioService(IPecasLaboratorioRepository pecasLaboratorioRepo, ISequenciaRepository sequenciaRepo)
        {
            _pecasLaboratorioRepo = pecasLaboratorioRepo;
            _sequenciaRepo = sequenciaRepo;
        }

        public ListViewModel ObterPorParametros(PecasLaboratorioParameters parameters)
        {
            var pecas = _pecasLaboratorioRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = pecas,
                TotalCount = pecas.TotalCount,
                CurrentPage = pecas.CurrentPage,
                PageSize = pecas.PageSize,
                TotalPages = pecas.TotalPages,
                HasNext = pecas.HasNext,
                HasPrevious = pecas.HasPrevious
            };

            return lista;
        }
    }
}
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoHistoricoService : IOrdemServicoHistoricoService
    {
        private readonly IOrdemServicoHistoricoRepository _osHistoricoRepo;

        public OrdemServicoHistoricoService(IOrdemServicoHistoricoRepository osHistoricoRepo)
        {
            _osHistoricoRepo = osHistoricoRepo;
        }

        public ListViewModel ObterPorParametros(OrdemServicoHistoricoParameters parameters)
        {
            var locais = _osHistoricoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = locais,
                TotalCount = locais.TotalCount,
                CurrentPage = locais.CurrentPage,
                PageSize = locais.PageSize,
                TotalPages = locais.TotalPages,
                HasNext = locais.HasNext,
                HasPrevious = locais.HasPrevious
            };

            return lista;
        }
    }
}

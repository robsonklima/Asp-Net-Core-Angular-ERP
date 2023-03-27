using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoPecaStatusService : IRelatorioAtendimentoPecaStatusService
    {
        private readonly IRelatorioAtendimentoPecaStatusRepository _relatorioAtendimentoPecaStatusRepo;

        public RelatorioAtendimentoPecaStatusService(
            IRelatorioAtendimentoPecaStatusRepository relatorioAtendimentoPecaStatusRepo
        )
        {
            _relatorioAtendimentoPecaStatusRepo = relatorioAtendimentoPecaStatusRepo;
        }

        public void Atualizar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _relatorioAtendimentoPecaStatusRepo.Atualizar(relatorioAtendimentoPecaStatus);
        }

        public void Criar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus)
        {
            _relatorioAtendimentoPecaStatusRepo.Criar(relatorioAtendimentoPecaStatus);
        }

        public void Deletar(int codRatpecasStatus)
        {
            _relatorioAtendimentoPecaStatusRepo.Deletar(codRatpecasStatus);
        }

        public RelatorioAtendimentoPecaStatus ObterPorCodigo(int codRatpecasStatus)
        {
            return _relatorioAtendimentoPecaStatusRepo.ObterPorCodigo(codRatpecasStatus);
        }

        public ListViewModel ObterPorParametros(RelatorioAtendimentoPecaStatusParameters parameters)
        {
            var relatorioAtendimentoPecaStatuss = _relatorioAtendimentoPecaStatusRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = relatorioAtendimentoPecaStatuss,
                TotalCount = relatorioAtendimentoPecaStatuss.TotalCount,
                CurrentPage = relatorioAtendimentoPecaStatuss.CurrentPage,
                PageSize = relatorioAtendimentoPecaStatuss.PageSize,
                TotalPages = relatorioAtendimentoPecaStatuss.TotalPages,
                HasNext = relatorioAtendimentoPecaStatuss.HasNext,
                HasPrevious = relatorioAtendimentoPecaStatuss.HasPrevious
            };

            return lista;
        }

    }
}

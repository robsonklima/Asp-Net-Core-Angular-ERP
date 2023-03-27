using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class RelatorioAtendimentoDetalhePecaStatusService : IRelatorioAtendimentoDetalhePecaStatusService
    {
        private readonly IRelatorioAtendimentoDetalhePecaStatusRepository _relatorioAtendimentoDetalhePecaStatusRepo;

        public RelatorioAtendimentoDetalhePecaStatusService(
            IRelatorioAtendimentoDetalhePecaStatusRepository relatorioAtendimentoDetalhePecaStatusRepo
        )
        {
            _relatorioAtendimentoDetalhePecaStatusRepo = relatorioAtendimentoDetalhePecaStatusRepo;
        }

        public void Atualizar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus)
        {
            _relatorioAtendimentoDetalhePecaStatusRepo.Atualizar(relatorioAtendimentoDetalhePecaStatus);
        }

        public void Criar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus)
        {
            _relatorioAtendimentoDetalhePecaStatusRepo.Criar(relatorioAtendimentoDetalhePecaStatus);
        }

        public void Deletar(int codRATDetalhesPecasStatus)
        {
            _relatorioAtendimentoDetalhePecaStatusRepo.Deletar(codRATDetalhesPecasStatus);
        }

        public RelatorioAtendimentoDetalhePecaStatus ObterPorCodigo(int codRATDetalhesPecasStatus)
        {
            return _relatorioAtendimentoDetalhePecaStatusRepo.ObterPorCodigo(codRATDetalhesPecasStatus);
        }

        public ListViewModel ObterPorParametros(RelatorioAtendimentoDetalhePecaStatusParameters parameters)
        {
            var relatorioAtendimentoDetalhePecaStatuss = _relatorioAtendimentoDetalhePecaStatusRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = relatorioAtendimentoDetalhePecaStatuss,
                TotalCount = relatorioAtendimentoDetalhePecaStatuss.TotalCount,
                CurrentPage = relatorioAtendimentoDetalhePecaStatuss.CurrentPage,
                PageSize = relatorioAtendimentoDetalhePecaStatuss.PageSize,
                TotalPages = relatorioAtendimentoDetalhePecaStatuss.TotalPages,
                HasNext = relatorioAtendimentoDetalhePecaStatuss.HasNext,
                HasPrevious = relatorioAtendimentoDetalhePecaStatuss.HasPrevious
            };

            return lista;
        }

    }
}

using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoRelatorioInstalacaoItemService : IOrdemServicoRelatorioInstalacaoItemService
    {
        private readonly IOrdemServicoRelatorioInstalacaoItemRepository _relatorioInstalacaoItemRepo;

        public OrdemServicoRelatorioInstalacaoItemService(IOrdemServicoRelatorioInstalacaoItemRepository relatorioAtendimentoItemRepo)
        {
            _relatorioInstalacaoItemRepo = relatorioAtendimentoItemRepo;
        }

        public List<OrdemServicoRelatorioInstalacaoItem> ObterItens()
        {
            return _relatorioInstalacaoItemRepo.ObterItens();
        }
    }
}

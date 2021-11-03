using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoRelatorioInstalacaoItemService
    {
        List<OrdemServicoRelatorioInstalacaoItem> ObterItens();
    }
}

using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoRepository
    {
        Instalacao Criar(Instalacao instalacao);
        PagedList<Instalacao> ObterPorParametros(InstalacaoParameters parameters);
        PagedList<InstalacaoView> ObterPorView(InstalacaoParameters parameters);
        void Deletar(int codigo);
        Instalacao Atualizar(Instalacao instalacao);
        Instalacao ObterPorCodigo(int codigo);
        List<ViewExportacaoInstalacao> ObterViewPorInstalacao(int[] instalacaoList);        
    }
}

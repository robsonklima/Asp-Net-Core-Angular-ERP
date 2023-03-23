using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IRelatorioAtendimentoDetalhePecaStatusService
    {
        ListViewModel ObterPorParametros(RelatorioAtendimentoDetalhePecaStatusParameters parameters);
        RelatorioAtendimentoDetalhePecaStatus ObterPorCodigo(int codigo);
        void Criar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus);
        void Deletar(int codRATDetalhesPecasStatus);
        void Atualizar(RelatorioAtendimentoDetalhePecaStatus relatorioAtendimentoDetalhePecaStatus);
    }
}

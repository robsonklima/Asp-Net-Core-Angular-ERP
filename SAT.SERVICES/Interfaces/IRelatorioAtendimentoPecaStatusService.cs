using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IRelatorioAtendimentoPecaStatusService
    {
        ListViewModel ObterPorParametros(RelatorioAtendimentoPecaStatusParameters parameters);
        RelatorioAtendimentoPecaStatus ObterPorCodigo(int codigo);
        void Criar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus);
        void Deletar(int codigo);
        void Atualizar(RelatorioAtendimentoPecaStatus relatorioAtendimentoPecaStatus);
    }
}

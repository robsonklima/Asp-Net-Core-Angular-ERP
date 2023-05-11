using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IRelatorioAtendimentoPOSService
    {
        ListViewModel ObterPorParametros(RelatorioAtendimentoPOSParameters parameters);
        RelatorioAtendimentoPOS Criar(RelatorioAtendimentoPOS relatorio);
        RelatorioAtendimentoPOS Deletar(int codigo);
        RelatorioAtendimentoPOS Atualizar(RelatorioAtendimentoPOS relatorio);
        RelatorioAtendimentoPOS ObterPorCodigo(int codigo);
    }
}

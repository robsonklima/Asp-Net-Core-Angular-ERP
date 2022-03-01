using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IRelatorioAtendimentoService
    {
        ListViewModel ObterPorParametros(RelatorioAtendimentoParameters parameters);
        RelatorioAtendimento Criar(RelatorioAtendimento relatorioAtendimento);
        void Deletar(int codigo);
        RelatorioAtendimento Atualizar(RelatorioAtendimento relatorioAtendimento);
        RelatorioAtendimento ObterPorCodigo(int codigo);
    }
}

using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IRelatorioAtendimentoDetalhePecaService
    {
        RelatorioAtendimentoDetalhePeca Criar(RelatorioAtendimentoDetalhePeca relatorioAtendimentoDetalhePeca);
        void Deletar(int codigo);
        void Atualizar(RelatorioAtendimentoDetalhePeca relatorioAtendimentoDetalhePeca);
        RelatorioAtendimentoDetalhePeca ObterPorCodigo(int codigo);
    }
}

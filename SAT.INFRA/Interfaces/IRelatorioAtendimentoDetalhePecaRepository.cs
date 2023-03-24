using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IRelatorioAtendimentoDetalhePecaRepository
    {
        void Deletar(int codRATDetalhePeca);
        void Criar(RelatorioAtendimentoDetalhePeca detalhePeca);
        void Atualizar(RelatorioAtendimentoDetalhePeca detalhePeca);
        RelatorioAtendimentoDetalhePeca ObterPorCodigo(int codigo);

    }
}

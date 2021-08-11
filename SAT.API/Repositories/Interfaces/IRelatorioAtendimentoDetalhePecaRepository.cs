using SAT.MODELS.Entities;

namespace SAT.API.Repositories.Interfaces
{
    public interface IRelatorioAtendimentoDetalhePecaRepository
    {
        void Deletar(int codRATDetalhePeca);
        RelatorioAtendimentoDetalhePeca ObterPorCodigo(int codigo);
    }
}

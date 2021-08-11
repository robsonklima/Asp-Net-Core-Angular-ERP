using SAT.MODELS.Entities;

namespace SAT.API.Repositories.Interfaces
{
    public interface IRelatorioAtendimentoDetalheRepository
    {
        void Deletar(int codRATDetalhe);
        RelatorioAtendimentoDetalhe ObterPorCodigo(int codigo);
    }
}

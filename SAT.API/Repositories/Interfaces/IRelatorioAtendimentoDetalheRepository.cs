using SAT.MODELS.Entities;

namespace SAT.API.Repositories.Interfaces
{
    public interface IRelatorioAtendimentoDetalheRepository
    {
        void Deletar(int codRATDetalhe);
        void Criar(RelatorioAtendimentoDetalhe detalhe);
        RelatorioAtendimentoDetalhe ObterPorCodigo(int codigo);
    }
}

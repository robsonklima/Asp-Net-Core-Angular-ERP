using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IRelatorioAtendimentoDetalheService
    {
        RelatorioAtendimentoDetalhe Criar(RelatorioAtendimentoDetalhe relatorioAtendimentoDetalhe);
        void Deletar(int codigo);
        void Atualizar(RelatorioAtendimentoDetalhe relatorioAtendimentoDetalhe);
        RelatorioAtendimentoDetalhe ObterPorCodigo(int codigo);
    }
}

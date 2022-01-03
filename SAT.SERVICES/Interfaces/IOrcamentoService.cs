using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoService
    {
        ListViewModel ObterPorParametros(OrcamentoParameters parameters);
        Orcamento Criar(Orcamento orcamento);
        void Deletar(int codigo);
        void Atualizar(Orcamento orcamento);
        Orcamento ObterPorCodigo(int codigo);
    }
}

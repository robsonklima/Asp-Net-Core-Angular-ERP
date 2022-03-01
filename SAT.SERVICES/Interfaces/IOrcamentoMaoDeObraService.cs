using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoMaoDeObraService
    {
        ListViewModel ObterPorParametros(OrcamentoMaoDeObraParameters parameters);
        OrcamentoMaoDeObra Criar(OrcamentoMaoDeObra orcamento);
        void Deletar(int codigo);
        OrcamentoMaoDeObra Atualizar(OrcamentoMaoDeObra orcamento);
        OrcamentoMaoDeObra ObterPorCodigo(int codigo);
    }
}

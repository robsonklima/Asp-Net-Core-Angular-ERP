using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoMaoDeObraRepository
    {
        PagedList<OrcamentoMaoDeObra> ObterPorParametros(OrcamentoMaoDeObraParameters parameters);
        void Criar(OrcamentoMaoDeObra orcamentoMaoDeObra);
        void Atualizar(OrcamentoMaoDeObra orcamentoMaoDeObra);
        void Deletar(int codigo);
        OrcamentoMaoDeObra ObterPorCodigo(int codigo);
    }
}

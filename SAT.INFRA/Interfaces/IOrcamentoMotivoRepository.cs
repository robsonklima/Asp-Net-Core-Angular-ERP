using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoMotivoRepository
    {
        PagedList<OrcamentoMotivo> ObterPorParametros(OrcamentoMotivoParameters parameters);
        void Criar(OrcamentoMotivo orcamentoMotivo);
        void Atualizar(OrcamentoMotivo orcamentoMotivo);
        void Deletar(int codOrcamentoMotivo);
        OrcamentoMotivo ObterPorCodigo(int codigo);
    }
}

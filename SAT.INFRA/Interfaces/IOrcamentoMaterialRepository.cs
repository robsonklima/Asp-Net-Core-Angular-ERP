using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoMaterialRepository
    {
        PagedList<OrcamentoMaterial> ObterPorParametros(OrcamentoMaterialParameters parameters);
        void Criar(OrcamentoMaterial orcamento);
        void Atualizar(OrcamentoMaterial orcamento);
        void Deletar(int codOrcamento);
        OrcamentoMaterial ObterPorCodigo(int codigo);
    }
}

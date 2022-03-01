using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoMaterialService
    {
        ListViewModel ObterPorParametros(OrcamentoMaterialParameters parameters);
        OrcamentoMaterial Criar(OrcamentoMaterial orcamento);
        void Deletar(int codigo);
        OrcamentoMaterial Atualizar(OrcamentoMaterial orcamento);
        OrcamentoMaterial ObterPorCodigo(int codigo);
    }
}

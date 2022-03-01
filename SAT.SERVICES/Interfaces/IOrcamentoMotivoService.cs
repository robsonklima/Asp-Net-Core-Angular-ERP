using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoMotivoService
    {
        ListViewModel ObterPorParametros(OrcamentoMotivoParameters parameters);
        OrcamentoMotivo Criar(OrcamentoMotivo orcamentoMotivo);
        void Deletar(int codigo);
        void Atualizar(OrcamentoMotivo orcamentoMotivo);
        OrcamentoMotivo ObterPorCodigo(int codigo);
    }
}

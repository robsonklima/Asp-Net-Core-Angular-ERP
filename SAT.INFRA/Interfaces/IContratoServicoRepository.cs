using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoServicoRepository
    {
        PagedList<ContratoServico> ObterPorParametros(ContratoServicoParameters parameters);
        ContratoServico ObterPorCodigo(int codContratoServico);
        void Criar(ContratoServico contratoServico);
        void Deletar(int codContratoServico);
        void Atualizar(ContratoServico contratoServico);
    }
}

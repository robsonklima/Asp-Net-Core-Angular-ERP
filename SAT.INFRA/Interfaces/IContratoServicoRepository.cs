using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoServicoRepository
    {
        PagedList<ContratoServico> ObterPorParametros(ContratoServicoParameters parameters);
        void Criar(ContratoServico contratoServico);
        void Deletar(int codContrato, int codContratoServico);
        void Atualizar(ContratoServico contratoServico);
        ContratoServico ObterPorCodigo(int codContrato, int codContratoServico);
    }
}

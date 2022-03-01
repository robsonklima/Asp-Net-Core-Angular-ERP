using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoSLARepository
    {
        PagedList<ContratoSLA> ObterPorParametros(ContratoSLAParameters parameters);
        void Criar(ContratoSLA contratoSLA);
        void Deletar(int codContrato, int codSLA);
        void Atualizar(ContratoSLA contratoSLA);
        ContratoSLA ObterPorCodigo(int codContrato, int codSLA);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IContratoReajusteRepository
    {
        PagedList<ContratoReajuste> ObterPorParametros(ContratoReajusteParameters parameters);
        void Criar(ContratoReajuste tipoCausa);
        void Atualizar(ContratoReajuste tipoCausa);
        void Deletar(int codContratoReajuste);
        ContratoReajuste ObterPorCodigo(int codigo);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoReajusteService
    {
        ListViewModel ObterPorParametros(ContratoReajusteParameters parameters);
        ContratoReajuste Criar(ContratoReajuste tipoCausa);
        void Deletar(int codigo);
        void Atualizar(ContratoReajuste tipoCausa);
        ContratoReajuste ObterPorCodigo(int codigo);
    }
}
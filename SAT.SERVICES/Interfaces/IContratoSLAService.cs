using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IContratoSLAService
    {
        ListViewModel ObterPorParametros(ContratoSLAParameters parameters);
        void Criar(ContratoSLA contratoSLA);
        void Deletar(int codContrato, int codSLA );
        void Atualizar(ContratoSLA contratoSLA);
        ContratoSLA ObterPorCodigo(int codContrato, int codSLA);
    }
}

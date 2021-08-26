using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAcordoNivelServicoService
    {
        ListViewModel ObterPorParametros(AcordoNivelServicoParameters parameters);
        AcordoNivelServico Criar(AcordoNivelServico ans);
        void Deletar(int codigo);
        void Atualizar(AcordoNivelServico ans);
        AcordoNivelServico ObterPorCodigo(int codigo);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ICausaService
    {
        ListViewModel ObterPorParametros(CausaParameters parameters);
        Causa Criar(Causa causa);
        void Deletar(int codigo);
        void Atualizar(Causa causa);
        Causa ObterPorCodigo(int codigo);
    }
}

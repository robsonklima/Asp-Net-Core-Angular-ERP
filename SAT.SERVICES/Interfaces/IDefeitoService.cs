using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IDefeitoService
    {
        ListViewModel ObterPorParametros(DefeitoParameters parameters);
        Defeito Criar(Defeito defeito);
        void Deletar(int codigo);
        void Atualizar(Defeito defeito);
        Defeito ObterPorCodigo(int codigo);
    }
}

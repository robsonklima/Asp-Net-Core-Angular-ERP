using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IPerfilSetorService
    {
        ListViewModel ObterPorParametros(PerfilSetorParameters parameters);
        PerfilSetor Criar(PerfilSetor perfilSetor);
        void Deletar(int codigo);
        void Atualizar(PerfilSetor perfilSetor);
        PerfilSetor ObterPorCodigo(int codigo);
    }
}

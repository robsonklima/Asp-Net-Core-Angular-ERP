using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface ISetorService
    {
        ListViewModel ObterPorParametros(SetorParameters parameters);
        Setor Criar(Setor setor);
        void Deletar(int codigo);
        void Atualizar(Setor setor);
        Setor ObterPorCodigo(int codigo);
    }
}

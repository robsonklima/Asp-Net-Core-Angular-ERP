using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORDefeitoService
    {
        ListViewModel ObterPorParametros(ORDefeitoParameters parameters);
        ORDefeito Criar(ORDefeito orDefeito);
        void Deletar(int codigo);
        void Atualizar(ORDefeito orDefeito);
        ORDefeito ObterPorCodigo(int codigo);
    }
}

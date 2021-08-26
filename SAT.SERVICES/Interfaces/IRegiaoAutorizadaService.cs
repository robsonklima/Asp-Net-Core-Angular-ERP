using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IRegiaoAutorizadaService
    {
        ListViewModel ObterPorParametros(RegiaoAutorizadaParameters parameters);
        RegiaoAutorizada Criar(RegiaoAutorizada regiaoAutorizada);
        void Deletar(int codigo);
        void Atualizar(RegiaoAutorizada regiaoAutorizada);
        RegiaoAutorizada ObterPorCodigo(int codigo);
    }
}

using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IFilialService
    {
        ListViewModel ObterPorParametros(FilialParameters parameters);
        Filial Criar(Filial filial);
        void Deletar(int codigo);
        void Atualizar(Filial filial);
        Filial ObterPorCodigo(int codigo);
    }
}

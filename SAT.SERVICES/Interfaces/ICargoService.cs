using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ICargoService
    {
        ListViewModel ObterPorParametros(CargoParameters parameters);
        Cargo Criar(Cargo cargo);
        void Deletar(int codigo);
        void Atualizar(Cargo cargo);
        Cargo ObterPorCodigo(int codigo);
        Cargo BuscaCargoPorNome(string nomeCargo);
    }
}

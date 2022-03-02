using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface ICargoRepository
    {
        PagedList<Cargo> ObterPorParametros(CargoParameters parameters);
        IQueryable<Cargo> ObterQuery(CargoParameters parameters);
        void Criar(Cargo cargo);
        void Atualizar(Cargo cargo);
        void Deletar(int codCargo);
        Cargo ObterPorCodigo(int codigo);
        Cargo BuscaCargoPorNome(string nomeCargo);
    }
}

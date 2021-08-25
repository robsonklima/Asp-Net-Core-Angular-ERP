using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IFeriadoRepository
    {
        PagedList<Feriado> ObterPorParametros(FeriadoParameters parameters);
        void Criar(Feriado feriado);
        void Atualizar(Feriado feriado);
        void Deletar(int codFeriado);
        Feriado ObterPorCodigo(int codigo);
    }
}

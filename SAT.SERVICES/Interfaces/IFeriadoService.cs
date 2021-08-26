using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IFeriadoService
    {
        ListViewModel ObterPorParametros(FeriadoParameters parameters);
        Feriado Criar(Feriado feriado);
        void Deletar(int codigo);
        void Atualizar(Feriado feriado);
        Feriado ObterPorCodigo(int codigo);
    }
}

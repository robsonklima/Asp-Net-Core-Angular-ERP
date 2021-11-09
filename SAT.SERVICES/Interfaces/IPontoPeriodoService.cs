using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoPeriodoService
    {
        ListViewModel ObterPorParametros(PontoPeriodoParameters parameters);
        PontoPeriodo Criar(PontoPeriodo pontoPeriodo);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodo pontoPeriodo);
        PontoPeriodo ObterPorCodigo(int codigo);
    }
}

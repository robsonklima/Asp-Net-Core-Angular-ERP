using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IAcordoNivelServicoRepository
    {
        PagedList<AcordoNivelServico> ObterPorParametros(AcordoNivelServicoParameters parameters);
        void Criar(AcordoNivelServico acordoNivelServico);
        void Atualizar(AcordoNivelServico acordoNivelServico);
        void Deletar(int codigo);
        AcordoNivelServico ObterPorCodigo(int codigo);
    }
}

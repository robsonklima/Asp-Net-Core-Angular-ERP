using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITipoServicoRepository
    {
        PagedList<TipoServico> ObterPorParametros(TipoServicoParameters parameters);
        void Criar(TipoServico tipoServico);
        void Atualizar(TipoServico tipoServico);
        void Deletar(int codServico);
        TipoServico ObterPorCodigo(int codigo);
    }
}

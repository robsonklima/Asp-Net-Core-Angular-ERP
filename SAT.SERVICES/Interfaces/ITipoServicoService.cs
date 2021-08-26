using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoServicoService
    {
        ListViewModel ObterPorParametros(TipoServicoParameters parameters);
        TipoServico Criar(TipoServico tipoServico);
        void Deletar(int codigo);
        void Atualizar(TipoServico tipoServico);
        TipoServico ObterPorCodigo(int codigo);
    }
}

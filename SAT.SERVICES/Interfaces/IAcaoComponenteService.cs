using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAcaoComponenteService
    {
        ListViewModel ObterPorParametros(AcaoComponenteParameters parameters);
        AcaoComponente Criar(AcaoComponente acao);
        void Deletar(int codigo);
        void Atualizar(AcaoComponente acao);
        AcaoComponente ObterPorCodigo(int codigo);
    }
}

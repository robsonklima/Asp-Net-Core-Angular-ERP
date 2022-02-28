using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IAutorizadaService
    {
        ListViewModel ObterPorParametros(AutorizadaParameters parameters);
        Autorizada Criar(Autorizada autorizada);
        void Deletar(int codigo);
        void Atualizar(Autorizada autorizada);
        Autorizada ObterPorCodigo(int codigo);
    }
}

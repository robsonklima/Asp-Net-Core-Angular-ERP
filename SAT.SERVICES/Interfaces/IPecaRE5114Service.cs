using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPecaRE5114Service
    {
        ListViewModel ObterPorParametros(PecaRE5114Parameters parameters);
        PecaRE5114 Criar(PecaRE5114 pecaRE5114);
        void Deletar(int codigo);
        void Atualizar(PecaRE5114 pecaRE5114);
        PecaRE5114 ObterPorCodigo(int codigo);
    }
}
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IUnidadeFederativaService
    {
        ListViewModel ObterPorParametros(UnidadeFederativaParameters parameters);
        UnidadeFederativa Criar(UnidadeFederativa unidadeFederativa);
        void Deletar(int codigo);
        void Atualizar(UnidadeFederativa unidadeFederativa);
        UnidadeFederativa ObterPorCodigo(int codigo);
    }
}

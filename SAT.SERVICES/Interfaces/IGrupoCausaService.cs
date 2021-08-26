using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IGrupoCausaService
    {
        ListViewModel ObterPorParametros(GrupoCausaParameters parameters);
        GrupoCausa Criar(GrupoCausa grupoCausa);
        void Deletar(int codigo);
        void Atualizar(GrupoCausa grupoCausa);
        GrupoCausa ObterPorCodigo(int codigo);
    }
}

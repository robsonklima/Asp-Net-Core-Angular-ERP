using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IPecaRE5114Repository
    {
        PagedList<PecaRE5114> ObterPorParametros(PecaRE5114Parameters parameters);
        void Criar(PecaRE5114 pecaRE5114);
        void Atualizar(PecaRE5114 pecaRE5114);
        void Deletar(int codPecaRe5114);
        PecaRE5114 ObterPorCodigo(int codigo);
    }
}

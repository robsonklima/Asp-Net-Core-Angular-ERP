using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAdendoRepository
    {
        PagedList<Adendo> ObterPorParametros(AdendoParameters parameters);
        Adendo Criar(Adendo data);
        Adendo Deletar(int cod);
        Adendo Atualizar(Adendo data);
        Adendo ObterPorCodigo(int cod);
    }
}

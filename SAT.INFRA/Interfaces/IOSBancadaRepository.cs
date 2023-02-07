using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOSBancadaRepository
    {
        PagedList<OSBancada> ObterPorParametros(OSBancadaParameters parameters);
        void Criar(OSBancada osBancada);
        void Atualizar(OSBancada osBancada);
        void Deletar(int codOsbancada);
        OSBancada ObterPorCodigo(int codigo);
    }
}

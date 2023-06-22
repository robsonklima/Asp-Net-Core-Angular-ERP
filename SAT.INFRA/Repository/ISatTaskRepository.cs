using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface ISatTaskRepository {
        PagedList<SatTask> ObterPorParametros(SatTaskParameters parameters);
        SatTask ObterPorCodigo(int CodSatTask);
        void Criar(SatTask SatTask);
        void Deletar(int codigoSatTask);
        void Atualizar(SatTask SatTask);
    }
}
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces {
    public interface IAuditoriaFotoRepository {
        PagedList<AuditoriaFoto> ObterPorParametros(AuditoriaFotoParameters parameters);
        AuditoriaFoto ObterPorCodigo(int CodAuditoriaFoto);
        void Criar(AuditoriaFoto auditoriaFoto);
        void Deletar(int codigoAuditoriaFoto);
        void Atualizar(AuditoriaFoto auditoriaFoto);

    }
}
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public  interface IAuditoriaFotoService
    {
        ListViewModel ObterPorParametros(AuditoriaFotoParameters parameters);
        AuditoriaFoto ObterPorCodigo(int codigo);
        void Criar(AuditoriaFoto auditoriaFoto);
        void Deletar(int codigoAuditoriaFoto);
        void Atualizar(AuditoriaFoto auditoriaFoto);
    }
}

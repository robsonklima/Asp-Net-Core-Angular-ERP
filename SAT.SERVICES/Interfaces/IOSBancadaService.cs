using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IOSBancadaService
    {
        ListViewModel ObterPorParametros(OSBancadaParameters parameters);
        OSBancada Criar(OSBancada osBancada);
        void Deletar(int codigo);
        void Atualizar(OSBancada osBancada);
        OSBancada ObterPorCodigo(int codigo);
    }
}

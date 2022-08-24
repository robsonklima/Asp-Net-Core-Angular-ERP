using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMensagemTecnicoService
    {
        ListViewModel ObterPorParametros(MensagemTecnicoParameters parameters);
        void Criar(MensagemTecnico msg);
        void Deletar(int codigo);
        MensagemTecnico Atualizar(MensagemTecnico msg);
        MensagemTecnico ObterPorCodigo(int codigo);
    }
}

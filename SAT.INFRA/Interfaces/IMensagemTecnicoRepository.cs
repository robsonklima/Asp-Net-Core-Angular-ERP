using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMensagemTecnicoRepository
    {
        PagedList<MensagemTecnico> ObterPorParametros(MensagemTecnicoParameters parameters);
        void Criar(MensagemTecnico mensagem);
        void Atualizar(MensagemTecnico mensagem);
        void Deletar(int codOrcamento);
        MensagemTecnico ObterPorCodigo(int codigo);
    }
}

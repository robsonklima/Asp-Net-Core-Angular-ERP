using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IRelatorioAtendimentoRepository
    {
        PagedList<RelatorioAtendimento> ObterPorParametros(RelatorioAtendimentoParameters parameters);
        void Criar(RelatorioAtendimento relatorioAtendimento);
        void Atualizar(RelatorioAtendimento relatorioAtendimento);
        void Deletar(int codRAT);
        RelatorioAtendimento ObterPorCodigo(int codigo);
    }
}

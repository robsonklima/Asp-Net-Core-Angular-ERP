using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IRelatorioAtendimentoRepository
    {
        PagedList<RelatorioAtendimento> ObterPorParametros(RelatorioAtendimentoParameters parameters);
        IQueryable<RelatorioAtendimento> ObterQuery(RelatorioAtendimentoParameters parameters);
        void Criar(RelatorioAtendimento relatorioAtendimento);
        void Atualizar(RelatorioAtendimento relatorioAtendimento);
        void Deletar(int codRAT);
        RelatorioAtendimento ObterPorCodigo(int codigo);
    }
}

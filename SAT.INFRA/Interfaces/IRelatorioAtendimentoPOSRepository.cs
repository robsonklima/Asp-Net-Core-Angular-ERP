using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IRelatorioAtendimentoPOSRepository
    {
        PagedList<RelatorioAtendimentoPOS> ObterPorParametros(RelatorioAtendimentoPOSParameters parameters);
        RelatorioAtendimentoPOS Criar(RelatorioAtendimentoPOS relatorio);
        RelatorioAtendimentoPOS Atualizar(RelatorioAtendimentoPOS relatorio);
        RelatorioAtendimentoPOS Deletar(int codRAT);
        RelatorioAtendimentoPOS ObterPorCodigo(int codigo);
    }
}

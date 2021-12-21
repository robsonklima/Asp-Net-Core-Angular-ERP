using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAT.INFRA.Context;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IAgendaTecnicoRepository
    {
        AgendaTecnico Criar(AgendaTecnico agenda);
        IQueryable<AgendaTecnico> ObterQuery(AgendaTecnicoParameters parameters);
        PagedList<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters);
        void Deletar(int codigo);
        AgendaTecnico Atualizar(AgendaTecnico agenda);
        Task<AgendaTecnico> AtualizarAsync(AgendaTecnico agenda);
        Task<bool> AtualizarListaAsync(List<AgendaTecnico> agenda);        
        AgendaTecnico ObterPorCodigo(int codigo);
    }
}

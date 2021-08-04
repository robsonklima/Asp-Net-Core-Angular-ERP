using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.API.Repositories.Interfaces
{
    public interface ITraducaoRepository
    {
        IEnumerable<Traducao> ObterPorParametros(int registros);
    }
}

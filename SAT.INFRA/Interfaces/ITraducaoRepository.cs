using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface ITraducaoRepository
    {
        IEnumerable<Traducao> ObterPorParametros(int registros);
    }
}

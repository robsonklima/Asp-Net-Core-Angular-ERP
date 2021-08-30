using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.SERVICES.Interfaces
{
    public interface IIndicadorService
    {
        List<Indicador> ObterIndicadoresOrdemServico(IndicadorParameters parameters);
    }
}

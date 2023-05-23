using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface INLogService
    {
        List<NLogRegistro> Obter(NLogParameters parameters);
        NLogRegistro Criar(NLogRegistro log);
    }
}

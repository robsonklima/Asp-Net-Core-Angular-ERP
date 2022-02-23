using System.Collections.Generic;
using IISLogParser;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IIISLogService
    {
        List<IISLogEvent> Get(IISLogParameters parameters);
    }
}

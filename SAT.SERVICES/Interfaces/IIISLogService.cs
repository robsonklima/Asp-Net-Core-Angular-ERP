using System.Collections.Generic;
using IISLogParser;

namespace SAT.SERVICES.Interfaces
{
    public interface IIISLogService
    {
        List<IISLogEvent> Get();
    }
}

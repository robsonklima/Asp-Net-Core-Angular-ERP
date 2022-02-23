using System;
using System.Collections.Generic;
using SAT.SERVICES.Interfaces;
using System.IO;
using System.Linq;
using IISLogParser;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Services
{
    public class IISLogService : IIISLogService
    {
        public List<IISLogEvent> Get(IISLogParameters parameters)
        {
            List<IISLogEvent> eventos = new List<IISLogEvent>();
            string path = Constants.IIS_LOG_PATH + "u_ex" +  (parameters.Data.ToString("yyMMdd") != DateTime.MinValue.ToString("yyMMdd") ? 
                            parameters.Data.ToString("yyMMdd") : DateTime.Now.ToString("yyMMdd")) + ".log";
            
            if (System.IO.File.Exists(path)) {
                string filename = Path.GetFileName(path);
                List<IISLogEvent> logs = new List<IISLogEvent>();
                using (ParserEngine parser = new ParserEngine(path))
                {
                    while (parser.MissingRecords)
                        foreach (var log in parser.ParseLog().ToList())
                            if (log.timeTaken > Constants.TEMPO_IISLOG_MS)
                                eventos.Add(log);
                }
            }

            return eventos.OrderByDescending(e => e.timeTaken).ToList();
        }
    }
}

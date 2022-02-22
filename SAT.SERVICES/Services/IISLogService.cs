using System;
using System.Collections.Generic;
using SAT.SERVICES.Interfaces;
using System.IO;
using System.Linq;
using IISLogParser;

namespace SAT.SERVICES.Services
{
    public class IISLogService : IIISLogService
    {
        public List<string> Get()
        {
            List<string> eventos = new List<string>();
            //string path = @"C:\inetpub\logs\LogFiles\W3SVC2\u_ex" + DateTime.Now.ToString("yyMMdd") + ".log";
            string path = @"C:\Users\rklima\Desktop\u_ex" + DateTime.Now.ToString("yyMMdd") + ".log";
            
            if (System.IO.File.Exists(path)) {
                string filename = Path.GetFileName(path);
                List<IISLogEvent> logs = new List<IISLogEvent>();
                using (ParserEngine parser = new ParserEngine(path))
                {
                    while (parser.MissingRecords)
                    {
                        logs = parser.ParseLog().ToList();
                    }
                }
            }

            return eventos;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using NLog;
using NLog.Fluent;
using System.Reflection;

namespace SAT.SERVICES.Services
{
    public class NLogService : INLogService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public List<NLogRegistro> Obter(NLogParameters parameters)
        {
            List<NLogRegistro> registros = new();
            string projectPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string file = projectPath + "\\Logs\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";

            if (System.IO.File.Exists(file))
            {
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(file))
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        NLogRegistro log = JsonConvert.DeserializeObject<NLogRegistro>(line);
                        registros.Add(log);
                    }

                    if (!string.IsNullOrWhiteSpace(parameters.Mensagem))
                        registros = registros.Where(r => r.Nested.Message.Contains(parameters.Filter)).ToList();
                }
            }

            return registros.OrderByDescending(r => r.Time).ToList();
        }

        public NLogRegistro Criar(NLogRegistro log)
        {
            _logger.Error()
                .Message($"{log.Nested.Message}")
                .Property("application", log.Nested.Application)
                .Write();

            return log;
        }
    }
}

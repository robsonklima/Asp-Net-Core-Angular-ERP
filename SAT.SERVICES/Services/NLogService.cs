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

namespace SAT.SERVICES.Services
{
    public class NLogService : INLogService
    {
        public List<NLogRegistro> Obter(NLogParameters parameters)
        {

            List<NLogRegistro> registros = new();

            foreach (string url in Constants.LOGS_URLS)
            {
                string file = url + DateTime.Now.ToString("yyyy-MM-dd") + ".json";

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

                            if (log.Time >= DateTime.Now.AddHours(-2))
                                registros.Add(log);
                        }
                    }
                }
            }

            return registros.OrderByDescending(r => r.Time).ToList();
        }
    }
}

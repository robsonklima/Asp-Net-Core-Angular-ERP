using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class NLogService : INLogService
    {
        public List<NLogRegistro> Obter(NLogParameters parameters)
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string file = path.Replace("file:\\", "") + "\\Logs\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".json";
            List<NLogRegistro> registros = new();

            if (System.IO.File.Exists(file)) {
                const Int32 BufferSize = 128;
                using (var fileStream = File.OpenRead(file))
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize)) {
                        String line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            NLogRegistro log = JsonConvert.DeserializeObject<NLogRegistro>(line);
                            registros.Add(log);
                        }
                    }
            }

            return registros;
        }
    }
}

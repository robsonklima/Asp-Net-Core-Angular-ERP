using System;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class MonitoramentoViewModel
    {
        public List<IntegracaoServidorModel> IntegracaoServidor = new();
        public List<StorageModel> StorageAPL1 = new();
        public List<StorageModel> StorageINT1 = new();

        public class IntegracaoServidorModel
        {
            public string Servidor { get; set; }
            public string Item { get; set; }
            public string Mensagem { get; set; }
            public string Tipo { get; set; }
            public double? EspacoEmGb { get; set; }
            public double? TamanhoEmGB { get; set; }
            public string Disco { get; set; }
            public DateTime? DataHoraProcessamento { get; set; }
            public DateTime? DataHoraCad { get; set; }

            public string Ociosidade
            {
                get
                {
                    return this.GetTime.ToString(@"hh\:mm");
                }
            }

            public bool ServidorOk
            {
                get
                {
                    return this.GetTime.TotalHours < 1.0d;
                }
            }

            private TimeSpan GetTime
            {
                get
                {
                    return (this.Tipo == "SERVICO" ? DateTime.Now.Subtract(this.DataHoraCad.Value) : DateTime.Now.Subtract(this.DataHoraProcessamento.Value));
                }
            }
        }

        public class StorageModel
        {
            public string Unidade { get; set; }
            public decimal Valor { get; set; }
        }
    }
}

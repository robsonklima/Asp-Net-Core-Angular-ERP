using System;

namespace SAT.MODELS.Entities
{
    public class MonitoramentoHistorico
    {
        public int CodHistLogAlerta { get; set; }
        public string Servidor { get; set; }
        public string Item { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public double? EmUso { get; set; }
        public double? Total { get; set; }
        public string Disco { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}

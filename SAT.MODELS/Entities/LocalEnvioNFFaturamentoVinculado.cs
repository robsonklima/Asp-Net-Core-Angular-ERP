using System;

namespace SAT.MODELS.Entities
{
    public class LocalEnvioNFFaturamentoVinculado {
        public int CodLocalEnvioNFFaturamento { get; set; }
        public int CodPosto { get; set; }
        public int CodContrato { get; set; }
        public byte? IndAdicionado { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }

        public LocalAtendimento LocalAtendimento { get; set; }
        public Contrato Contrato { get; set; }
    }
}
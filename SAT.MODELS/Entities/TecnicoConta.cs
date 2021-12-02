using System;

namespace SAT.MODELS.Entities
{
    public class TecnicoConta
    {
        public int CodTecnicoConta { get; set; }
        public int CodTecnico { get; set; }
        public string NumBanco { get; set; }
        public string NumAgencia { get; set; }
        public string NumConta { get; set; }
        public byte IndPadrao { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
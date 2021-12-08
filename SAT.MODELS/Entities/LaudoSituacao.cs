using System;

namespace SAT.MODELS.Entities
{
    public class LaudoSituacao
    {
        public int CodLaudoSituacao { get; set; }
        public int CodLaudo { get; set; }
        public string Causa { get; set; }
        public string Acao { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}
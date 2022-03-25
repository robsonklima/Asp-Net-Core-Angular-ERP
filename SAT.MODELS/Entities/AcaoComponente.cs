using System;

namespace SAT.MODELS.Entities
{
    public class AcaoComponente
    {
        public int CodAcaoComponente { get; set; }
        public string CodECausa { get; set; }
        public int CodAcao { get; set; }
        public byte? Selecionado { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public Causa Causa { get; set; }
        public Acao Acao { get; set; }
    }
}

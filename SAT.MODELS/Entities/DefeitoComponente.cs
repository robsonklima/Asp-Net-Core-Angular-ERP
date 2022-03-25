using System;

namespace SAT.MODELS.Entities
{
    public class DefeitoComponente
    {
        public int CodDefeitoComponente { get; set; }
        public string CodECausa { get; set; }
        public int CodDefeito { get; set; }
        public byte? Selecionado { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
        public Causa Causa { get; set; }
        public Defeito Defeito { get; set; }
    }
}

using System;

namespace SAT.MODELS.Entities
{
    public class AuditoriaVeiculoAcessorio
    {
        public int CodAuditoriaVeiculoAcessorio { get; set; }
        public int CodAuditoriaVeiculo { get; set; }
        public string Nome { get; set; }
        public byte Selecionado { get; set; }
        public string Justificativa { get; set; }
        public DateTime? DataHoraCad { get; set; }

    }
}
using System;

namespace SAT.MODELS.Entities
{
    public class VeiculoCombustivel
    {
        public int CodVeiculoCombustivel { get; set; }
        public string Tipo { get; set; }
        public decimal ValorKm { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
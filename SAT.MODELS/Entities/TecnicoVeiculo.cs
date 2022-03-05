using System;

namespace SAT.MODELS.Entities
{
    public class TecnicoVeiculo
    {
        public int CodTecnicoVeiculo { get; set; }
        public int CodTecnico { get; set; }
        public int CodVeiculoCombustivel { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public byte IndPadrao { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public VeiculoCombustivel Combustivel { get; set; }
    }
}
using System;

namespace SAT.MODELS.Entities
{
    public class ClientePeca
    {
        public int CodClientePeca { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodPeca { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? ValorIPI { get; set; }
        public decimal? VlrSubstituicaoNovo { get; set; }
        public decimal? vlrBaseTroca { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
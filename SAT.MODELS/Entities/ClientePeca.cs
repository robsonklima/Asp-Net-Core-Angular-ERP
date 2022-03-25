using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        public decimal? VlrBaseTroca { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        [NotMapped]
        public Peca Peca { get; set; }
        [NotMapped]
        public Cliente Cliente { get; set; }
        [NotMapped]
        public Contrato Contrato { get; set; }
    }
}
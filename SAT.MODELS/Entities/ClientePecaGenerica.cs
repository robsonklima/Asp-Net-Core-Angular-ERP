using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class ClientePecaGenerica
    {
        public int CodClientePecaGenerica { get; set; }
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
    }
}
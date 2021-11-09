using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities 
{
    public class PontoPeriodo
    {
        [Key]
        public int CodPontoPeriodo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int CodPontoPeriodoStatus { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public int? CodPontoPeriodoModoAprovacao { get; set; }
        public int CodPontoPeriodoIntervaloAcessoData { get; set; }
    }
}
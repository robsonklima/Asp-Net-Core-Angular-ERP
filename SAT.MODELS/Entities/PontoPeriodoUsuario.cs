using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class PontoPeriodoUsuario
    {
        [Key]
        public int CodPontoPeriodoUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        public string CodUsuario { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
        public int CodPontoPeriodoUsuarioStatus { get; set; }
    }
}
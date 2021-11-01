using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class Ponto
    {
        [Key]
        public int CodPonto { get; set; }
        public string CodUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        public DateTime? DataPonto { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public int CodPontoTipoHora { get; set; }
        public byte? IndAprovado { get; set; }
        public byte IndRevisado { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime? DataHoraAprov { get; set; }
        public string Observacao { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public string CodUsuarioAprov { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class PontoMovel
    {
        [Key]
        public int CodPontoMovel { get; set; }
        public int CodPontoMovelTipoHorario { get; set; }
        public int CodPontoPeriodo { get; set; }
        public string CodUsuario { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public byte? IndManual { get; set; }
        public byte? IndRegistroSemConexaoDados { get; set; }
        public byte? IndDataHoraAutomaticaAtivada { get; set; }
        public byte? IndFusoHorarioAutomaticoAtivado { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string Observacao { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class PontoUsuarioData
    {
        [Key]
        public int CodPontoUsuarioData { get; set; }
        public string CodUsuario { get; set; }
        public int CodPontoPeriodo { get; set; }
        public int CodPontoUsuarioDataStatus { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
        public int CodPontoUsuarioDataStatusAcesso { get; set; }
    }
}
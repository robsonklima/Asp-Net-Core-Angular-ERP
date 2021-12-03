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
        
        [NotMapped]
        public DateTime? HorasExtras { get; set; }

        [ForeignKey("CodPontoUsuarioDataStatus")]
        public PontoUsuarioDataStatus PontoUsuarioDataStatus { get; set; }

        [ForeignKey("CodPontoPeriodo")]
        public PontoPeriodo PontoPeriodo { get; set; }

        [ForeignKey("CodPontoUsuarioDataStatusAcesso")]
        public PontoUsuarioDataStatusAcesso PontoUsuarioDataStatusAcesso { get; set; }

        [ForeignKey("CodUsuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("CodPontoUsuarioData")]
        public List<PontoUsuarioDataDivergencia> Divergencias { get; set; }

        [NotMapped]        
        public List<PontoUsuario> PontosUsuario { get; set; }
    }
}
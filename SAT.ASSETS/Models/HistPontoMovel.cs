using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistPontoMovel")]
    public partial class HistPontoMovel
    {
        [Key]
        public int CodHistPontoMovel { get; set; }
        public int CodPontoMovel { get; set; }
        public int CodPontoMovelTipoHorario { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraRegistro { get; set; }
        public byte? IndManual { get; set; }
        public byte? IndRegistroSemConexaoDados { get; set; }
        public byte? IndDataHoraAutomaticaAtivada { get; set; }
        public byte? IndFusoHorarioAutomaticoAtivado { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(8000)]
        public string Observacao { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHistCad { get; set; }
    }
}

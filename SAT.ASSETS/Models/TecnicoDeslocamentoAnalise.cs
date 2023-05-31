using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TecnicoDeslocamentoAnalise")]
    public partial class TecnicoDeslocamentoAnalise
    {
        [Key]
        public int CodTecnicoDeslocamentoAnalise { get; set; }
        public int CodTecnico { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        [Column("CodRAT")]
        public int? CodRat { get; set; }
        [StringLength(60)]
        public string NomeLocal { get; set; }
        [StringLength(20)]
        public string Origem { get; set; }
        [Required]
        [StringLength(50)]
        public string LatOrigem { get; set; }
        [Required]
        [StringLength(50)]
        public string LngOrigem { get; set; }
        [StringLength(20)]
        public string Destino { get; set; }
        [Required]
        [StringLength(50)]
        public string LatDestino { get; set; }
        [Required]
        [StringLength(50)]
        public string LngDestino { get; set; }
        public double DistanciaKm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraPrimeiroAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUltimoAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraPrimeiroPonto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraUltimoPonto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [StringLength(300)]
        public string Notificacao { get; set; }
    }
}

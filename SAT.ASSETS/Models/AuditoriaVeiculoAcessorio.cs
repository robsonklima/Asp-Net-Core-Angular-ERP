using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("AuditoriaVeiculoAcessorio")]
    public partial class AuditoriaVeiculoAcessorio
    {
        [Key]
        public int CodAuditoriaVeiculoAcessorio { get; set; }
        public int? CodAuditoriaVeiculo { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        public byte? Selecionado { get; set; }
        [StringLength(350)]
        public string Justificativa { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}

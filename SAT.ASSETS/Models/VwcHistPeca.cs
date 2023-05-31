using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcHistPeca
    {
        [Required]
        [StringLength(24)]
        public string CodMagnus { get; set; }
        public int CodHistorico { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        [StringLength(50)]
        public string StatusPeca { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraHist { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraAtualizacaoValor { get; set; }
    }
}

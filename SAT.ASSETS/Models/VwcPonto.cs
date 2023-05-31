using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPonto
    {
        public int CodPonto { get; set; }
        public int CodPontoPeriodo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraFim { get; set; }
        public int? QtdHora { get; set; }
        public int CodPontoTipoHora { get; set; }
        [StringLength(20)]
        public string CodUsuarioAprov { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodFilialPonto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
    }
}

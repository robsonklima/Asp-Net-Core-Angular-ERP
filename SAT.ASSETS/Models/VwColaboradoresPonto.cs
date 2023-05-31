using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwColaboradoresPonto
    {
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [StringLength(6)]
        public string NumCracha { get; set; }
        public int? CodCargo { get; set; }
        public int? CodTurno { get; set; }
        public int? CodFilialPonto { get; set; }
        [StringLength(50)]
        public string NomeCargo { get; set; }
        [StringLength(50)]
        public string DescTurno { get; set; }
        [StringLength(50)]
        public string RazaoSocial { get; set; }
        public int IndPontoDivergente { get; set; }
        public int IndPontoValidado { get; set; }
        public int IndAguardandoValidacao { get; set; }
    }
}

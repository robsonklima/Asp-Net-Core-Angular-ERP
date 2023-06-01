using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDadosDashboardDisponibilidadeTechApp
    {
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int CodTecnico { get; set; }
        public int IndFerias { get; set; }
        public byte IndAtivo { get; set; }
        public int? QtdChamadosAtendidosSomenteCorretivos { get; set; }
        public int? QtdChamadosAtendidosNaoCorretivos { get; set; }
    }
}

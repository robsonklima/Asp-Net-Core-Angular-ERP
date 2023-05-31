using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcDashboardLiderTecnico
    {
        public int? CodFilial { get; set; }
        [StringLength(20)]
        public string CodUsuarioLider { get; set; }
        [StringLength(50)]
        public string Lider { get; set; }
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodTecnico { get; set; }
        public byte? IndAtivoTecnico { get; set; }
        public int IndFeriasTecnico { get; set; }
        public byte IndAtivoUsuario { get; set; }
        public byte? IndPontoUsuario { get; set; }
        public int? ItensTrabalho { get; set; }
        public int SinalSatelite { get; set; }
    }
}

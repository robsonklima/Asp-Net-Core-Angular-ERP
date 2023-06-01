using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DashboardTecnicoDisponibilidadeBackup")]
    public partial class DashboardTecnicoDisponibilidadeBackup
    {
        public string NomeUsuario { get; set; }
        [StringLength(25)]
        public string CodUsuario { get; set; }
        public int? CodTecnico { get; set; }
        public int? CodFilial { get; set; }
        public int? IndFerias { get; set; }
        public int? IndUsuarioAtivo { get; set; }
        public int? IndTecnicoAtivo { get; set; }
        public int? QtdPonto { get; set; }
        public int? SinalSatelite { get; set; }
        public int? ItensTrabalho { get; set; }
        public int? QtdChamadosTransferidos { get; set; }
        public int? QtdChamadosAtendidosTodasIntervencoes { get; set; }
        public int? QtdChamadosAtendidosSomenteCorretivos { get; set; }
        public int? QtdChamadosAtendidosSomentePreventivos { get; set; }
        public int? QtdChamadosAtendidosSomenteInstalacao { get; set; }
        public int? QtdChamadosAtendidosSomenteEngenharia { get; set; }
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int? QtdChamadosAtendidosTodasIntervencoesDia { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PontoTipoHora")]
    public partial class PontoTipoHora
    {
        public PontoTipoHora()
        {
            PontoColaboradors = new HashSet<PontoColaborador>();
            Pontos = new HashSet<Ponto>();
        }

        [Key]
        public int CodPontoTipoHora { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeTipoHora { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(PontoColaborador.CodPontoTipoHoraNavigation))]
        public virtual ICollection<PontoColaborador> PontoColaboradors { get; set; }
        [InverseProperty(nameof(Ponto.CodPontoTipoHoraNavigation))]
        public virtual ICollection<Ponto> Pontos { get; set; }
    }
}

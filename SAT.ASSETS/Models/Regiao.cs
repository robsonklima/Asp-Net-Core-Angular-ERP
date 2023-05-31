using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Regiao")]
    public partial class Regiao
    {
        public Regiao()
        {
            AutorizadaRepasses = new HashSet<AutorizadaRepasse>();
            AutorizadaUfcidadePos = new HashSet<AutorizadaUfcidadePo>();
            PlantaoTecnicoRegiaos = new HashSet<PlantaoTecnicoRegiao>();
            ValorServicos = new HashSet<ValorServico>();
        }

        [Key]
        public int CodRegiao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRegiao { get; set; }
        public byte IndAtivo { get; set; }

        [InverseProperty(nameof(AutorizadaRepasse.CodRegiaoNavigation))]
        public virtual ICollection<AutorizadaRepasse> AutorizadaRepasses { get; set; }
        [InverseProperty(nameof(AutorizadaUfcidadePo.CodRegiaoNavigation))]
        public virtual ICollection<AutorizadaUfcidadePo> AutorizadaUfcidadePos { get; set; }
        [InverseProperty(nameof(PlantaoTecnicoRegiao.CodRegiaoNavigation))]
        public virtual ICollection<PlantaoTecnicoRegiao> PlantaoTecnicoRegiaos { get; set; }
        [InverseProperty(nameof(ValorServico.CodRegiaoNavigation))]
        public virtual ICollection<ValorServico> ValorServicos { get; set; }
    }
}

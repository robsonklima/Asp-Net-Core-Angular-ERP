using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PlantaoTecnicoRegiao")]
    public partial class PlantaoTecnicoRegiao
    {
        [Key]
        public int CodPlantaoTecnicoRegiao { get; set; }
        public int CodPlantaoTecnico { get; set; }
        public int CodRegiao { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [ForeignKey(nameof(CodPlantaoTecnico))]
        [InverseProperty(nameof(PlantaoTecnico.PlantaoTecnicoRegiaos))]
        public virtual PlantaoTecnico CodPlantaoTecnicoNavigation { get; set; }
        [ForeignKey(nameof(CodRegiao))]
        [InverseProperty(nameof(Regiao.PlantaoTecnicoRegiaos))]
        public virtual Regiao CodRegiaoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("PlantaoTecnico")]
    public partial class PlantaoTecnico
    {
        [Key]
        public int CodPlantaoTecnico { get; set; }
        public int CodTecnico { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataPlantao { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        //[ForeignKey(nameof(CodTecnico))]
        //[InverseProperty(nameof(Tecnico.PlantaoTecnicos))]
        //public virtual Tecnico CodTecnicoNavigation { get; set; }
        //[InverseProperty(nameof(PlantaoTecnicoCliente.CodPlantaoTecnicoNavigation))]
        //public virtual ICollection<PlantaoTecnicoCliente> PlantaoTecnicoClientes { get; set; }
        //[InverseProperty(nameof(PlantaoTecnicoRegiao.CodPlantaoTecnicoNavigation))]
        //public virtual ICollection<PlantaoTecnicoRegiao> PlantaoTecnicoRegiaos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PlantaoTecnicoCliente")]
    public partial class PlantaoTecnicoCliente
    {
        [Key]
        public int CodPlantaoTecnicoCliente { get; set; }
        public int CodPlantaoTecnico { get; set; }
        public int CodCliente { get; set; }
        public byte IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.PlantaoTecnicoClientes))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodPlantaoTecnico))]
        [InverseProperty(nameof(PlantaoTecnico.PlantaoTecnicoClientes))]
        public virtual PlantaoTecnico CodPlantaoTecnicoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("VeiculoCombustivel")]
    public partial class VeiculoCombustivel
    {
        public VeiculoCombustivel()
        {
            TecnicoVeiculos = new HashSet<TecnicoVeiculo>();
        }

        [Key]
        public int CodVeiculoCombustivel { get; set; }
        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorKm { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [InverseProperty(nameof(TecnicoVeiculo.CodVeiculoCombustivelNavigation))]
        public virtual ICollection<TecnicoVeiculo> TecnicoVeiculos { get; set; }
    }
}

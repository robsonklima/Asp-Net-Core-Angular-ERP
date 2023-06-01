using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TecnicoVeiculo")]
    public partial class TecnicoVeiculo
    {
        [Key]
        public int CodTecnicoVeiculo { get; set; }
        public int CodTecnico { get; set; }
        public int CodVeiculoCombustivel { get; set; }
        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }
        public int Ano { get; set; }
        [Required]
        [StringLength(8)]
        public string Placa { get; set; }
        public byte IndPadrao { get; set; }
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

        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.TecnicoVeiculos))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
        [ForeignKey(nameof(CodVeiculoCombustivel))]
        [InverseProperty(nameof(VeiculoCombustivel.TecnicoVeiculos))]
        public virtual VeiculoCombustivel CodVeiculoCombustivelNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OS_SUPORTE")]
    public partial class OsSuporte
    {
        [Key]
        public int CodSuporte { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCadastro { get; set; }
        public int CodStatus { get; set; }
        [Column("OSMantis")]
        public int Osmantis { get; set; }
        public string Acao { get; set; }
        [StringLength(100)]
        public string NomeEquipamento { get; set; }
        [StringLength(50)]
        public string NumSerie { get; set; }
        [StringLength(10)]
        public string Filial { get; set; }
        public int? NumReincidencia { get; set; }
        public int? IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFim { get; set; }
    }
}

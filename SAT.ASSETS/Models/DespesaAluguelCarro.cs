using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaAluguelCarro")]
    public partial class DespesaAluguelCarro
    {
        [Key]
        public int CodDespesaAluguelCarro { get; set; }
        public int CodTecnico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        public byte? IndAtivo { get; set; }

        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.DespesaAluguelCarros))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
    }
}

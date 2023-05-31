using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OrcMaoObra")]
    public partial class OrcMaoObra
    {
        [Key]
        public int CodOrcMaoObra { get; set; }
        public int? CodOrc { get; set; }
        public int? PrevisaoHoras { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorHoraTecnica { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Redutor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string UsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodOrc))]
        [InverseProperty(nameof(Orc.OrcMaoObras))]
        public virtual Orc CodOrcNavigation { get; set; }
    }
}

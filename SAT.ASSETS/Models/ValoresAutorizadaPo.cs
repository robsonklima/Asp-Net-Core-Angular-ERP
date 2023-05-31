using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ValoresAutorizadaPOS")]
    public partial class ValoresAutorizadaPo
    {
        [Key]
        [Column("CodValoresAutorizadaPOS")]
        public int CodValoresAutorizadaPos { get; set; }
        public int CodAutorizada { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorFechados { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? ValorCancelados { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataInicioVigencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFimVigencia { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int? KmInicial { get; set; }
        public int? KmFinal { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }

        [ForeignKey(nameof(CodAutorizada))]
        [InverseProperty(nameof(Autorizadum.ValoresAutorizadaPos))]
        public virtual Autorizadum CodAutorizadaNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.ValoresAutorizadaPos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ChamadoHist")]
    public partial class ChamadoHist
    {
        [Key]
        public int CodChamadoHist { get; set; }
        public int CodChamado { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        public int CodAcaoChamado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }
        [StringLength(2000)]
        public string Descricao { get; set; }

        [ForeignKey(nameof(CodAcaoChamado))]
        [InverseProperty(nameof(AcaoChamado.ChamadoHists))]
        public virtual AcaoChamado CodAcaoChamadoNavigation { get; set; }
        [ForeignKey(nameof(CodChamado))]
        [InverseProperty(nameof(Chamado.ChamadoHists))]
        public virtual Chamado CodChamadoNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.ChamadoHists))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}

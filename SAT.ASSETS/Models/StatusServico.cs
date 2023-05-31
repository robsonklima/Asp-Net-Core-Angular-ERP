using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusServico")]
    public partial class StatusServico
    {
        public StatusServico()
        {
            FecharOspos = new HashSet<FecharOspo>();
        }

        [Key]
        public int CodStatusServico { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        public int? IndPendente { get; set; }
        public int? IndEncerrado { get; set; }
        [StringLength(7)]
        public string CorFundo { get; set; }
        [StringLength(7)]
        public string CorFonte { get; set; }
        public int? TamFonte { get; set; }
        public int? IndNegrito { get; set; }
        [StringLength(5)]
        public string Abrev { get; set; }
        public int? IndServico { get; set; }
        public int? CodTraducao { get; set; }
        public int? IndAtivo { get; set; }
        [Column("IndLiberadoOSBloqueado")]
        public int? IndLiberadoOsbloqueado { get; set; }

        [InverseProperty("CodStatusServicoNavigation")]
        public virtual CorStatusServicoPo CorStatusServicoPo { get; set; }
        [InverseProperty(nameof(FecharOspo.CodStatusServicoNavigation))]
        public virtual ICollection<FecharOspo> FecharOspos { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class CorStatusServicoPo
    {
        [Key]
        public int CodCorStatusServicoPos { get; set; }
        public int CodStatusServico { get; set; }
        [Required]
        [StringLength(50)]
        public string Cor { get; set; }

        [ForeignKey(nameof(CodStatusServico))]
        [InverseProperty(nameof(StatusServico.CorStatusServicoPo))]
        public virtual StatusServico CodStatusServicoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoCancelamentoBanrisul")]
    public partial class MotivoCancelamentoBanrisul
    {
        public MotivoCancelamentoBanrisul()
        {
            MotivoCancelamentoDeParas = new HashSet<MotivoCancelamentoDePara>();
        }

        [Key]
        public int CodMotivoCancelamentoBanrisul { get; set; }
        [Required]
        [StringLength(3)]
        public string SiglaMotivo { get; set; }
        [Required]
        [StringLength(34)]
        public string NomeMotivo { get; set; }

        [InverseProperty(nameof(MotivoCancelamentoDePara.CodMotivoCancelamentoBanrisulNavigation))]
        public virtual ICollection<MotivoCancelamentoDePara> MotivoCancelamentoDeParas { get; set; }
    }
}

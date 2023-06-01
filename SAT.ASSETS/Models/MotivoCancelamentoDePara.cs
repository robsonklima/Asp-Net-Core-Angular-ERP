using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("MotivoCancelamentoDePara")]
    public partial class MotivoCancelamentoDePara
    {
        [Key]
        public int CodMotivoCancelamentoDePara { get; set; }
        public int CodMotivoCancelamento { get; set; }
        public int CodMotivoCancelamentoBanrisul { get; set; }

        [ForeignKey(nameof(CodMotivoCancelamentoBanrisul))]
        [InverseProperty(nameof(MotivoCancelamentoBanrisul.MotivoCancelamentoDeParas))]
        public virtual MotivoCancelamentoBanrisul CodMotivoCancelamentoBanrisulNavigation { get; set; }
        [ForeignKey(nameof(CodMotivoCancelamento))]
        [InverseProperty(nameof(MotivoCancelamento.MotivoCancelamentoDePara))]
        public virtual MotivoCancelamento CodMotivoCancelamentoNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Representante")]
    public partial class Representante
    {
        public Representante()
        {
            NotaFaturamentoSicredis = new HashSet<NotaFaturamentoSicredi>();
            PatrimonioPos = new HashSet<PatrimonioPo>();
        }

        [Key]
        public int CodRepresentante { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeRepresentante { get; set; }
        [StringLength(50)]
        public string CentroCusto { get; set; }

        [InverseProperty(nameof(NotaFaturamentoSicredi.CodRepresentanteNavigation))]
        public virtual ICollection<NotaFaturamentoSicredi> NotaFaturamentoSicredis { get; set; }
        [InverseProperty(nameof(PatrimonioPo.CodRepresentanteNavigation))]
        public virtual ICollection<PatrimonioPo> PatrimonioPos { get; set; }
    }
}

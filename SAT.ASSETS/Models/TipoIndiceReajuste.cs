using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoIndiceReajuste")]
    public partial class TipoIndiceReajuste
    {
        public TipoIndiceReajuste()
        {
            ContratoReajustes = new HashSet<ContratoReajuste>();
        }

        [Key]
        public int CodTipoIndiceReajuste { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeTipoIndiceReajuste { get; set; }
        public byte? IndAtivo { get; set; }

        [InverseProperty(nameof(ContratoReajuste.CodTipoIndiceReajusteNavigation))]
        public virtual ICollection<ContratoReajuste> ContratoReajustes { get; set; }
    }
}

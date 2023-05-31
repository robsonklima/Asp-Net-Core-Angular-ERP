using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoContrato")]
    public partial class TipoContrato
    {
        public TipoContrato()
        {
            Contratos = new HashSet<Contrato>();
        }

        [Key]
        public int CodTipoContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeTipoContrato { get; set; }

        [InverseProperty(nameof(Contrato.CodTipoContratoNavigation))]
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}

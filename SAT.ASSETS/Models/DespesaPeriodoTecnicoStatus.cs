using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaPeriodoTecnicoStatus")]
    public partial class DespesaPeriodoTecnicoStatus
    {
        public DespesaPeriodoTecnicoStatus()
        {
            DespesaPeriodoTecnicos = new HashSet<DespesaPeriodoTecnico>();
        }

        [Key]
        public int CodDespesaPeriodoTecnicoStatus { get; set; }
        [Required]
        [StringLength(30)]
        public string NomeDespesaPeriodoTecnicoStatus { get; set; }

        [InverseProperty(nameof(DespesaPeriodoTecnico.CodDespesaPeriodoTecnicoStatusNavigation))]
        public virtual ICollection<DespesaPeriodoTecnico> DespesaPeriodoTecnicos { get; set; }
    }
}

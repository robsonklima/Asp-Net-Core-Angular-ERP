using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SolicitantePatrimonio")]
    public partial class SolicitantePatrimonio
    {
        public SolicitantePatrimonio()
        {
            PatrimonioPos = new HashSet<PatrimonioPo>();
        }

        [Key]
        public int CodSolicitantePatrimonio { get; set; }
        [Required]
        [StringLength(200)]
        public string NomeSolicitantePatrimonio { get; set; }

        [InverseProperty(nameof(PatrimonioPo.CodSolicitantePatrimonioNavigation))]
        public virtual ICollection<PatrimonioPo> PatrimonioPos { get; set; }
    }
}

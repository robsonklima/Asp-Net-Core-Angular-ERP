using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class TipoRotum
    {
        public TipoRotum()
        {
            Tecnicos = new HashSet<Tecnico>();
        }

        [Key]
        public int CodTipoRota { get; set; }
        [StringLength(100)]
        public string NomeTipoRota { get; set; }

        [InverseProperty(nameof(Tecnico.CodTipoRotaNavigation))]
        public virtual ICollection<Tecnico> Tecnicos { get; set; }
    }
}

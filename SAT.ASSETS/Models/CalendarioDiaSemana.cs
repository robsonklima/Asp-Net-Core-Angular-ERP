using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CalendarioDiaSemana")]
    public partial class CalendarioDiaSemana
    {
        public CalendarioDiaSemana()
        {
            CalendarioDia = new HashSet<CalendarioDium>();
        }

        [Key]
        public int CodCalendarioDiaSemana { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeDiaSemana { get; set; }

        [InverseProperty(nameof(CalendarioDium.CodCalendarioDiaSemanaNavigation))]
        public virtual ICollection<CalendarioDium> CalendarioDia { get; set; }
    }
}

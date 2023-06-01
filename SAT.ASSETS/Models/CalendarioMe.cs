using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class CalendarioMe
    {
        public CalendarioMe()
        {
            CalendarioDia = new HashSet<CalendarioDium>();
        }

        [Key]
        public int CodCalendarioMes { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeMes { get; set; }

        [InverseProperty(nameof(CalendarioDium.CodCalendarioMesNavigation))]
        public virtual ICollection<CalendarioDium> CalendarioDia { get; set; }
    }
}

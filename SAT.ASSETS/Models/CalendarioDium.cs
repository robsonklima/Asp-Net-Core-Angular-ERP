using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class CalendarioDium
    {
        public CalendarioDium()
        {
            CalendarioDiaFeriados = new HashSet<CalendarioDiaFeriado>();
        }

        [Key]
        public int CodCalendarioDia { get; set; }
        public int NumDia { get; set; }
        public int CodCalendarioDiaSemana { get; set; }
        public int CodCalendarioMes { get; set; }
        public int NumAno { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Data { get; set; }

        [ForeignKey(nameof(CodCalendarioDiaSemana))]
        [InverseProperty(nameof(CalendarioDiaSemana.CalendarioDia))]
        public virtual CalendarioDiaSemana CodCalendarioDiaSemanaNavigation { get; set; }
        [ForeignKey(nameof(CodCalendarioMes))]
        [InverseProperty(nameof(CalendarioMe.CalendarioDia))]
        public virtual CalendarioMe CodCalendarioMesNavigation { get; set; }
        [InverseProperty(nameof(CalendarioDiaFeriado.CodCalendarioDiaNavigation))]
        public virtual ICollection<CalendarioDiaFeriado> CalendarioDiaFeriados { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CalendarioDiaFeriado")]
    public partial class CalendarioDiaFeriado
    {
        [Key]
        public int CodCalendarioDiaFeriado { get; set; }
        public int CodCalendarioDia { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFeriado { get; set; }
        public int CodPais { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int? CodCidade { get; set; }

        [ForeignKey(nameof(CodCalendarioDia))]
        [InverseProperty(nameof(CalendarioDium.CalendarioDiaFeriados))]
        public virtual CalendarioDium CodCalendarioDiaNavigation { get; set; }
        [ForeignKey(nameof(CodCidade))]
        [InverseProperty(nameof(Cidade.CalendarioDiaFeriados))]
        public virtual Cidade CodCidadeNavigation { get; set; }
        [ForeignKey(nameof(CodUf))]
        [InverseProperty(nameof(Uf.CalendarioDiaFeriados))]
        public virtual Uf CodUfNavigation { get; set; }
    }
}

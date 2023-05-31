using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class FeriadosNacionai
    {
        [Key]
        public int CodFeriadoNacional { get; set; }
        [Required]
        [StringLength(100)]
        public string NomeFeriado { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime Data { get; set; }
        public int CodPais { get; set; }

        [ForeignKey(nameof(CodPais))]
        [InverseProperty(nameof(Pai.FeriadosNacionais))]
        public virtual Pai CodPaisNavigation { get; set; }
    }
}

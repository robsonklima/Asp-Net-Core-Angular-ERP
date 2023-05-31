using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class FeriadosParciai
    {
        public int CodFeriadoParcial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFeriado { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DataUtilInicio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DataUtilFim { get; set; }
        public int? CodPais { get; set; }
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int? CodCidade { get; set; }
    }
}

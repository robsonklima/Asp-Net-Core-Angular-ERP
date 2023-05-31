using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcOsClienteCoordenadorContrato
    {
        [Required]
        [Column("CodOS")]
        [StringLength(50)]
        public string CodOs { get; set; }
        [Required]
        [Column("DataHoraAberturaOS")]
        [StringLength(50)]
        public string DataHoraAberturaOs { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCoordenadorContrato { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
    }
}

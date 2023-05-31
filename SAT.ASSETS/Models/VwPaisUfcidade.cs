using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwPaisUfcidade
    {
        public int CodCidade { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeCidade { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        [Required]
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        public int CodPais { get; set; }
    }
}

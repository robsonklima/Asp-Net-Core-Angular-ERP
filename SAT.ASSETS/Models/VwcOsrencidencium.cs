using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcOsrencidencium
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int? NumReincidencia { get; set; }
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        [Required]
        [StringLength(3)]
        public string NumBanco { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(4)]
        public string Dcposto { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeLocal { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
    }
}

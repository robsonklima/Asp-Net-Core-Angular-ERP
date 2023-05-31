using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcClienteLocalAtendimento
    {
        public int CodCliente { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        public int CodPosto { get; set; }
        [Required]
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Required]
        [Column("DCPosto")]
        [StringLength(2)]
        public string Dcposto { get; set; }
    }
}

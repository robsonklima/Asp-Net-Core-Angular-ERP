using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TipoLocalAtendimento")]
    public partial class TipoLocalAtendimento
    {
        public int CodTipoLocalAtendimento { get; set; }
        [Required]
        [StringLength(20)]
        public string NomeTipoLocalAtendimento { get; set; }
        public byte IndAtivo { get; set; }
    }
}

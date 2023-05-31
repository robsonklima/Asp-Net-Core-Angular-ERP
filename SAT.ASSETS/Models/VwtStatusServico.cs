using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtStatusServico
    {
        public int CodStatusServico { get; set; }
        [StringLength(50)]
        public string NomeStatusServico { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}

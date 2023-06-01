using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcPatRegiao
    {
        public int CodAutorizada { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        public int CodRegiao { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeRegiao { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwPecaNailton2006
    {
        [Required]
        [StringLength(20)]
        public string CodMagnus { get; set; }
        [Required]
        [StringLength(80)]
        public string NomePeca { get; set; }
        public int QtdUtilizada { get; set; }
    }
}

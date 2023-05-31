using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtTipoServico
    {
        public int CodServico { get; set; }
        [Column("CodETipoServico")]
        [StringLength(3)]
        public string CodEtipoServico { get; set; }
        [StringLength(50)]
        public string NomeServico { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtPedidoPecaStatus
    {
        public int CodPedidoPecaStatus { get; set; }
        [Required]
        [StringLength(20)]
        public string SiglaStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeStatus { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}

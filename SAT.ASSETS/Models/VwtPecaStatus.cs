using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtPecaStatus
    {
        public int CodPecaStatus { get; set; }
        [Required]
        [StringLength(20)]
        public string SiglaStatus { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeStatus { get; set; }
        [StringLength(250)]
        public string MsgStatus { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
    }
}

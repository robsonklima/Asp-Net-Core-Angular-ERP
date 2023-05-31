using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwtMotivoAgendamento
    {
        public int CodMotivo { get; set; }
        [StringLength(500)]
        public string DescricaoMotivo { get; set; }
        [Required]
        [StringLength(5)]
        public string Culture { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

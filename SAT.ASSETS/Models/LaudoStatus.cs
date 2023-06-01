using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("LaudoStatus")]
    public partial class LaudoStatus
    {
        [Key]
        public int CodLaudoStatus { get; set; }
        [StringLength(30)]
        public string NomeStatus { get; set; }
        public byte? IndAtivo { get; set; }
    }
}

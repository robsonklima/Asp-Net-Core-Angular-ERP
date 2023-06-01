using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class LogGp
    {
        [Key]
        public int CodLog { get; set; }
        [StringLength(40)]
        public string SimCardMobile { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraLog { get; set; }
        [StringLength(512)]
        public string DescricaoLog { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}

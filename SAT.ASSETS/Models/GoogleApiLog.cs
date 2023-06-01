using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("GoogleApiLog")]
    public partial class GoogleApiLog
    {
        public int CodGoogleApiLog { get; set; }
        [StringLength(150)]
        public string Titulo { get; set; }
        [StringLength(500)]
        public string Detalhes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}

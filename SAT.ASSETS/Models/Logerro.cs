using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("_LOGERROS")]
    public partial class Logerro
    {
        public string Titulo { get; set; }
        public string NomeMetodo { get; set; }
        [StringLength(200)]
        public string CodUsuario { get; set; }
        public string Mensagem { get; set; }
        public string StackTrace { get; set; }
        public string Query { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraErro { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("UsuarioFoto")]
    public partial class UsuarioFoto
    {
        [StringLength(80)]
        public string CodUsuario { get; set; }
        [StringLength(150)]
        public string NomeFoto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
    }
}

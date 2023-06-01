using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("InstalacaoImportacaoLog")]
    public partial class InstalacaoImportacaoLog
    {
        public int CodInstalacaoImportacaoLog { get; set; }
        [StringLength(50)]
        public string NumSerie { get; set; }
        [StringLength(250)]
        public string Msg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCadastro { get; set; }
    }
}

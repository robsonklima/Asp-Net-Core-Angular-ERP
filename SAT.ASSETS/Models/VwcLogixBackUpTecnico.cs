using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixBackUpTecnico
    {
        public int CodTecnico { get; set; }
        public int CodPeca { get; set; }
        public int BackupMinimo { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        public int? DataEmissaoAntiga { get; set; }
    }
}

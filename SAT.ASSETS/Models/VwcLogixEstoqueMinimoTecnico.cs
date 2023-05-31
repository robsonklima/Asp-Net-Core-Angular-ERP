using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogixEstoqueMinimoTecnico
    {
        public int CodTecnico { get; set; }
        public int CodPeca { get; set; }
        [Column("QTD")]
        public int Qtd { get; set; }
        public int? CodFilial { get; set; }
    }
}

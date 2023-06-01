using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("KITPadraoTecnico")]
    public partial class KitpadraoTecnico
    {
        [Column("CodKITPadraoTecnico")]
        public int CodKitpadraoTecnico { get; set; }
        public int CodTecnico { get; set; }
        [Column("CodKITPadrao")]
        public int CodKitpadrao { get; set; }
    }
}

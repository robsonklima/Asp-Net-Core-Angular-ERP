using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ClienteContratoCoord")]
    public partial class ClienteContratoCoord
    {
        [StringLength(255)]
        public string Cliente { get; set; }
        [StringLength(255)]
        public string Contrato { get; set; }
        [StringLength(255)]
        public string Coordenador { get; set; }
        [Column("E-mail")]
        [StringLength(255)]
        public string EMail { get; set; }
    }
}

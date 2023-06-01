using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ClienteIntervPerfil")]
    public partial class ClienteIntervPerfil
    {
        public int CodClienteIntervPerfil { get; set; }
        public int? CodCliente { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodPerfil { get; set; }
    }
}

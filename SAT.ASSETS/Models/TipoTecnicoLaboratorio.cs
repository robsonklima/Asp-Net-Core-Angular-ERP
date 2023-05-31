using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("TipoTecnicoLaboratorio")]
    public partial class TipoTecnicoLaboratorio
    {
        [StringLength(50)]
        public string CodUsuario { get; set; }
        [StringLength(50)]
        public string TipoItens { get; set; }
        public int? IndAtivo { get; set; }
    }
}

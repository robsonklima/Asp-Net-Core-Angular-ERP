using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcOsreincidencium
    {
        public int? NumReincidencia { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
    }
}

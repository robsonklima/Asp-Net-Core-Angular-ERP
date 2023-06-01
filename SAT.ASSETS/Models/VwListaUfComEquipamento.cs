using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwListaUfComEquipamento
    {
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int CodCliente { get; set; }
        [Column("CodSLA")]
        public int CodSla { get; set; }
    }
}

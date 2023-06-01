using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoMercado")]
    public partial class TipoMercado
    {
        [Key]
        public int CodTipoMercado { get; set; }
        [StringLength(30)]
        public string NomeTipoMercado { get; set; }
    }
}

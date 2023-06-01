using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("CausaImprodutividade")]
    public partial class CausaImprodutividade
    {
        [Key]
        public int CodCausaImprodutividade { get; set; }
        public int CodImprodutividade { get; set; }
        public int CodProtocolo { get; set; }
        [Column("indAtivo")]
        public int? IndAtivo { get; set; }
    }
}

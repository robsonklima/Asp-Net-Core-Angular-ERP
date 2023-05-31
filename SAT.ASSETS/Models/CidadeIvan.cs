using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("CidadeIvan")]
    public partial class CidadeIvan
    {
        [Column("CodUF")]
        public int? CodUf { get; set; }
        public int? CodCidade { get; set; }
        [StringLength(50)]
        public string NomeCidade { get; set; }
    }
}

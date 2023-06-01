using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("FaturamentoPOS")]
    public partial class FaturamentoPo
    {
        [Column("CNPJ")]
        public string Cnpj { get; set; }
        public string NomeFantasia { get; set; }
    }
}

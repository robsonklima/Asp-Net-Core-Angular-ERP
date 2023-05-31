using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("POS_CPFCNPJ")]
    public partial class PosCpfcnpj
    {
        [Column("CPFCNPJ")]
        public string Cpfcnpj { get; set; }
    }
}

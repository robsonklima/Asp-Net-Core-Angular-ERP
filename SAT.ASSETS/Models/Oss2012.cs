using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("oss2012")]
    public partial class Oss2012
    {
        [Column("CodOS")]
        public double? CodOs { get; set; }
    }
}

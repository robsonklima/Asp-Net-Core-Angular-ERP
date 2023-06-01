using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ControlePOSSerieTMP")]
    public partial class ControlePosserieTmp
    {
        public int Cod { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
    }
}

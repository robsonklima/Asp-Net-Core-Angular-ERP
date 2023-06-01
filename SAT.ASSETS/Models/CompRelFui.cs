using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("CompRelFui")]
    public partial class CompRelFui
    {
        public int CodPosto { get; set; }
        public int CodTecnico { get; set; }
    }
}

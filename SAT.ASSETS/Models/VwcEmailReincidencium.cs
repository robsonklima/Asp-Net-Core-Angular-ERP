using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcEmailReincidencium
    {
        public string Email { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
    }
}

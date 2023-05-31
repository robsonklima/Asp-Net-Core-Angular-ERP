using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwvEMailsReincidencium1
    {
        public int CodControleReincidencia { get; set; }
        [StringLength(50)]
        public string NomeFantasia { get; set; }
        public int NumReincidencia { get; set; }
        [StringLength(1000)]
        public string Email { get; set; }
    }
}

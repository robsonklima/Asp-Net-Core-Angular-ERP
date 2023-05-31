using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("RegiaoAutorizadaNew")]
    public partial class RegiaoAutorizadaNew
    {
        public int? Regiao { get; set; }
        [StringLength(50)]
        public string Cidade { get; set; }
    }
}

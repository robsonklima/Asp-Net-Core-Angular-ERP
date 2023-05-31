using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FilialRegiaoDisp")]
    public partial class FilialRegiaoDisp
    {
        [Key]
        public int CodFilialRegiaoDisp { get; set; }
        public int? CodFilial { get; set; }
        [StringLength(25)]
        public string NomeFilial { get; set; }
        [StringLength(50)]
        public string NomeRegiao { get; set; }
    }
}

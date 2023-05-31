using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TraducaoInterface")]
    [Index(nameof(Portugues), Name = "IX_TraducaoInterface", IsUnique = true)]
    public partial class TraducaoInterface
    {
        public int CodTraducaoInterface { get; set; }
        [Key]
        [StringLength(300)]
        public string Portugues { get; set; }
        [StringLength(300)]
        public string Ingles { get; set; }
        [StringLength(300)]
        public string Espanhol { get; set; }
    }
}

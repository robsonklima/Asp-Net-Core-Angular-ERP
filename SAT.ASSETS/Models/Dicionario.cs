using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("Dicionario")]
    public partial class Dicionario
    {
        [Key]
        [Column("codDicionario")]
        public int CodDicionario { get; set; }
        [StringLength(50)]
        public string Chave { get; set; }
        [StringLength(500)]
        public string Portugues { get; set; }
        [StringLength(500)]
        public string Ingles { get; set; }
        [StringLength(500)]
        public string Espanhol { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("StatusDocumentacao")]
    public partial class StatusDocumentacao
    {
        [Key]
        public int CodStatusDocumentocao { get; set; }
        [StringLength(500)]
        public string NomeStatusDocumentacao { get; set; }
    }
}

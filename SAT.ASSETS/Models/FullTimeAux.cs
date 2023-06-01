using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FullTimeAux")]
    public partial class FullTimeAux
    {
        [Key]
        public int CodFullTimeAux { get; set; }
        public int? Janela01A { get; set; }
        public int? Janela02 { get; set; }
        public int? Janela01B { get; set; }
        public int CodCliente { get; set; }
    }
}

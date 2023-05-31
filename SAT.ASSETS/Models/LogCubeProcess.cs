using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("log_cube_process")]
    public partial class LogCubeProcess
    {
        [Key]
        [Column("data", TypeName = "datetime")]
        public DateTime Data { get; set; }
        [Column("sucesso")]
        public int? Sucesso { get; set; }
        [Column("obs")]
        [StringLength(256)]
        public string Obs { get; set; }
    }
}

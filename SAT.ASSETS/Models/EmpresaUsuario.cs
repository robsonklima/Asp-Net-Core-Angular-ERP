using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EmpresaUsuario")]
    public partial class EmpresaUsuario
    {
        [Key]
        public int CodEmpresa { get; set; }
        [Key]
        [StringLength(20)]
        public string CodUsuario { get; set; }
    }
}

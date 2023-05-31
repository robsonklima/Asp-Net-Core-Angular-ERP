using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Parque_POS_Banrisul")]
    public partial class ParquePosBanrisul
    {
        [Column("CNPJ")]
        [StringLength(50)]
        public string Cnpj { get; set; }
        [Column("MODELO")]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Column("HABILITADO")]
        [StringLength(1)]
        public string Habilitado { get; set; }
    }
}

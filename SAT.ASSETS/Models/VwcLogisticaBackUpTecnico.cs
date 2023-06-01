using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcLogisticaBackUpTecnico
    {
        [Column("CPFLogix")]
        [StringLength(20)]
        public string Cpflogix { get; set; }
        public int CodTecnico { get; set; }
        public int CodPeca { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataModificacao { get; set; }
        public int BackupMinimo { get; set; }
        [Required]
        [StringLength(24)]
        public string CodMagnus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("tb_LogisticaBackupMinimoPadrao")]
    public partial class TbLogisticaBackupMinimoPadrao
    {
        [Required]
        [Column("codMagnus")]
        [StringLength(50)]
        public string CodMagnus { get; set; }
        [Column("codFilial")]
        public int? CodFilial { get; set; }
        [Column("codTecnico")]
        public int? CodTecnico { get; set; }
        public int BackupMinimo { get; set; }
    }
}

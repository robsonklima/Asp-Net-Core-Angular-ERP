using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("tb_LogisticaBackUpTecnico")]
    public partial class TbLogisticaBackUpTecnico
    {
        public int CodTecnico { get; set; }
        public int CodPeca { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataModificacao { get; set; }
        public int BackupMinimo { get; set; }
    }
}

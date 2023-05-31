using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("tb_LogisticaBackUPFilial")]
    public partial class TbLogisticaBackUpfilial
    {
        [Key]
        public int CodLogisticaBkpFilial { get; set; }
        public int CodFilial { get; set; }
        public int CodPeca { get; set; }
        public int BackupMinimo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataModificacao { get; set; }
    }
}

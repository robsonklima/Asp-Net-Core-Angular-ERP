using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("VariacaoEquipamento")]
    public partial class VariacaoEquipamento
    {
        [Key]
        public int CodVariacaoEquipamento { get; set; }
        public int? CodCliente { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        [StringLength(20)]
        public string NomeVariacao { get; set; }
        [StringLength(20)]
        public string LocalVariacao { get; set; }
    }
}

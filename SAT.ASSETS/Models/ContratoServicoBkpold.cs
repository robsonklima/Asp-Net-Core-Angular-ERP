using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ContratoServicoBKPOLD")]
    public partial class ContratoServicoBkpold
    {
        public int CodContratoServico { get; set; }
        public int? CodContrato { get; set; }
        public int CodServico { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        [Column(TypeName = "money")]
        public decimal Valor { get; set; }
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [StringLength(20)]
        public string DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [StringLength(20)]
        public string DataHoraManut { get; set; }
        [Column("CodUsuarioCadastro_DEL")]
        [StringLength(20)]
        public string CodUsuarioCadastroDel { get; set; }
        [Column("DataHoraCadastro_DEL")]
        [StringLength(20)]
        public string DataHoraCadastroDel { get; set; }
        [Column("CodUsuarioManutencao_DEL")]
        [StringLength(20)]
        public string CodUsuarioManutencaoDel { get; set; }
        [Column("DataHoraManutencao_DEL")]
        [StringLength(20)]
        public string DataHoraManutencaoDel { get; set; }
    }
}

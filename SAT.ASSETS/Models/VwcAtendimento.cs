using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcAtendimento
    {
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraInicio { get; set; }
        public int? CodStatusServico { get; set; }
        public int? CodEquipContrato { get; set; }
        public int CodCliente { get; set; }
        public int CodPosto { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int CodTecnico { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        [Required]
        [StringLength(3)]
        public string Garantia { get; set; }
        [Required]
        [StringLength(3)]
        public string Ativo { get; set; }
        [Column("AnoOS")]
        public int? AnoOs { get; set; }
        [Column("MesOS")]
        [StringLength(2)]
        public string MesOs { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
    }
}

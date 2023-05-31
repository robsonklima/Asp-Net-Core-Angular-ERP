using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcSla1
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int CodTipoIntervencao { get; set; }
        public int CodStatusServico { get; set; }
        public int CodCliente { get; set; }
        public int CodPosto { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        [Column("CodSLA")]
        public int CodSla { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFim { get; set; }
        public int CodCidade { get; set; }
        [Column("CodUF")]
        public int CodUf { get; set; }
        public int CodPais { get; set; }
        [Required]
        [Column("PA")]
        [StringLength(30)]
        public string Pa { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Required]
        [StringLength(3)]
        public string Maior5Dias { get; set; }
    }
}

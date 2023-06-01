using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("OSAtendidas")]
    public partial class Osatendida
    {
        [Key]
        [Column("CodOSAtendidas")]
        public int CodOsatendidas { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? AnoMes { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column("DataHoraFechamentoOS", TypeName = "datetime")]
        public DateTime? DataHoraFechamentoOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraFechamentoOsAtendidas { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodCliente { get; set; }
        public int? CodPosto { get; set; }
        public int? CodFilial { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodTipoIntervencao { get; set; }
        public int? CodStatusServico { get; set; }
        public byte? IndServico { get; set; }

        [ForeignKey("CodRegiao,CodAutorizada,CodFilial")]
        [InverseProperty(nameof(RegiaoAutorizadum.Osatendida))]
        public virtual RegiaoAutorizadum Cod { get; set; }
        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.Osatendida))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodEquipContrato))]
        [InverseProperty(nameof(EquipamentoContrato.Osatendida))]
        public virtual EquipamentoContrato CodEquipContratoNavigation { get; set; }
    }
}

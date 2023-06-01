using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TipoIntervencao")]
    public partial class TipoIntervencao
    {
        public TipoIntervencao()
        {
            OspossicrediCobrancaAtendimentos = new HashSet<OspossicrediCobrancaAtendimento>();
        }

        [Key]
        public int CodTipoIntervencao { get; set; }
        [Column("CodETipoIntervencao")]
        [StringLength(5)]
        public string CodEtipoIntervencao { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        public byte? CalcPreventivaIntervenc { get; set; }
        public byte? VerificaReincidenciaInt { get; set; }
        public int? CodTraducao { get; set; }
        public byte? IndAtivo { get; set; }

        [InverseProperty(nameof(OspossicrediCobrancaAtendimento.CodTipoIntervencaoNavigation))]
        public virtual ICollection<OspossicrediCobrancaAtendimento> OspossicrediCobrancaAtendimentos { get; set; }
    }
}

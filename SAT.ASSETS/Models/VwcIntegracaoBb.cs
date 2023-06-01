using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcIntegracaoBb
    {
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(10)]
        public string DataInicioAtendimento { get; set; }
        [StringLength(8)]
        public string HoraInicioAtendimento { get; set; }
        [StringLength(10)]
        public string DataFimAtendimento { get; set; }
        [StringLength(8)]
        public string HoraFimAtendimento { get; set; }
        [StringLength(10)]
        public string DataFinal { get; set; }
        [StringLength(8)]
        public string HoraFinal { get; set; }
        public string NomeTecnico { get; set; }
        [StringLength(10)]
        public string DataAgendamento { get; set; }
        [StringLength(8)]
        public string HoraAgendamento { get; set; }
        [Column("ManutencaoOS")]
        public int ManutencaoOs { get; set; }
        [Column("NumRAT")]
        [StringLength(20)]
        public string NumRat { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        [Column("SituacaoOS")]
        public int SituacaoOs { get; set; }
        [StringLength(6)]
        public string NumAgencia { get; set; }
        [Column("CodECausa")]
        [StringLength(5)]
        public string CodEcausa { get; set; }
        [Required]
        [StringLength(3)]
        public string DeParaCausa { get; set; }
    }
}

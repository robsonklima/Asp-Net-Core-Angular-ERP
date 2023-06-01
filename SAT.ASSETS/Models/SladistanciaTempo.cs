using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SLADistanciaTempo")]
    public partial class SladistanciaTempo
    {
        [Key]
        [Column("CodSLADistanciaTempo")]
        public int CodSladistanciaTempo { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        [Column("DistanciaKM_DEL")]
        public short? DistanciaKmDel { get; set; }
        [Column("DistanciaLimiteInicioAtendimento_DEL")]
        public short? DistanciaLimiteInicioAtendimentoDel { get; set; }
        [Column("DistanciaAdicionalAtendimento_DEL")]
        public short? DistanciaAdicionalAtendimentoDel { get; set; }
        [Column("TempoAdicionalAtendimento_DEL")]
        public int? TempoAdicionalAtendimentoDel { get; set; }
        [Column("DistanciaLimiteInicioReparo_DEL")]
        public short? DistanciaLimiteInicioReparoDel { get; set; }
        [Column("DistanciaAdicionalReparo_DEL")]
        public short? DistanciaAdicionalReparoDel { get; set; }
        [Column("TempoAdicionalReparo_DEL")]
        public int? TempoAdicionalReparoDel { get; set; }
        public short? DistanciaLimiteInicioSolucao { get; set; }
        public short? DistanciaAdicionalSolucao { get; set; }
        public int? TempoAdicionalSolucao { get; set; }

        [ForeignKey(nameof(CodSla))]
        [InverseProperty(nameof(SlaNew.SladistanciaTempos))]
        public virtual SlaNew CodSlaNavigation { get; set; }
    }
}

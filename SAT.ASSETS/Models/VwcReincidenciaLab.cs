using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReincidenciaLab
    {
        [Column("reincidencia")]
        public int? Reincidencia { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("CodOR")]
        public int CodOr { get; set; }
        [StringLength(80)]
        public string NomePeca { get; set; }
        [StringLength(24)]
        public string CodMagnus { get; set; }
        [Column("Data Hora Cadastro")]
        [StringLength(4000)]
        public string DataHoraCadastro { get; set; }
        [Column("Data Conf. Log.")]
        [StringLength(4000)]
        public string DataConfLog { get; set; }
        [Column("Data Conf. Lab.")]
        [StringLength(4000)]
        public string DataConfLab { get; set; }
        [Column("Técnico Reparador")]
        [StringLength(50)]
        public string TécnicoReparador { get; set; }
        [StringLength(200)]
        public string DefeitoRelatado { get; set; }
        [StringLength(200)]
        public string RelatoSolucao { get; set; }
        [Column("Data Início Reparo")]
        [StringLength(4000)]
        public string DataInícioReparo { get; set; }
        [Column("Data Fim Reparo")]
        [StringLength(4000)]
        public string DataFimReparo { get; set; }
        [Column("Status Item")]
        [StringLength(100)]
        public string StatusItem { get; set; }
        [Column("mes")]
        public int? Mes { get; set; }
        [Column("ano")]
        public int? Ano { get; set; }
    }
}

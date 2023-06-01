using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Analise_Caixa_Contrato0077")]
    public partial class AnaliseCaixaContrato0077
    {
        [Column("CO_CHAMADO")]
        [StringLength(50)]
        public string CoChamado { get; set; }
        [Column("DE_PERIODO", TypeName = "datetime")]
        public DateTime? DePeriodo { get; set; }
        [Column("NU_RAT")]
        public double? NuRat { get; set; }
        [Column("CO_CHAMADO1")]
        [StringLength(50)]
        public string CoChamado1 { get; set; }
        [Column("NO_TIPO_MODALIDADE")]
        [StringLength(255)]
        public string NoTipoModalidade { get; set; }
        [Column("VR_MULTA", TypeName = "decimal(10, 2)")]
        public decimal? VrMulta { get; set; }
        [Column("DT_INICIO_ATENDIMENTO", TypeName = "datetime")]
        public DateTime? DtInicioAtendimento { get; set; }
        [Column("DT_TERMINO_ATENDIMENTO", TypeName = "datetime")]
        public DateTime? DtTerminoAtendimento { get; set; }
        [Column("ATRASO")]
        [StringLength(255)]
        public string Atraso { get; set; }
    }
}

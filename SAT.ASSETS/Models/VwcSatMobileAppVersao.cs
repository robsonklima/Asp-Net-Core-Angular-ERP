using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcSatMobileAppVersao
    {
        [Required]
        [Column("nome_filial")]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("qtd_tecnicos_ativos_app_atualizado")]
        public int? QtdTecnicosAtivosAppAtualizado { get; set; }
        [Column("qtd_tecnicos_ativos")]
        public int? QtdTecnicosAtivos { get; set; }
        [Column("percentual_tecnicos_ativos_app_atualizado", TypeName = "decimal(10, 1)")]
        public decimal? PercentualTecnicosAtivosAppAtualizado { get; set; }
    }
}

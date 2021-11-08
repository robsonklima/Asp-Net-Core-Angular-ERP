using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaConfiguracaoCombustivel
    {
        [Key]
        public int CodDespesaConfiguracaoCombustivel { get; set; }
        public int? CodFilial { get; set; }
        public int? CodUf { get; set; }
        public double? PrecoLitro { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
    }
}
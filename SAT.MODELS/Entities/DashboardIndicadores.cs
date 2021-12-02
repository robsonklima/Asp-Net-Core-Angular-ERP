using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DashboardIndicadores
    {
        [Key]
        public int CodDashboardIndicadores { get; set; }
        public string NomeIndicador { get; set; }
        public string DadosJson { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
    }
}

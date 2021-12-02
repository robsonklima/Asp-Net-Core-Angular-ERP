using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAT.MODELS.Entities
{
    [Keyless]
    [Table("DashboardDisponibilidade")]
    public partial class DashboardDisponibilidade
    {
        public string Regiao { get; set; }
        public int? Criticidade { get; set; }
        public string Filial { get; set; }
        public decimal? PrcTotalFilial { get; set; }
        public int? CodFilial { get; set; }
    }
}

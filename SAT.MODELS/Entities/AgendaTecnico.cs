using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnico
    {
        [Key]
        public int CodAgendaTecnico { get; set; }
        public int CodTecnico { get; set; }
        public int CodOS { get; set; }
        public OrdemServico OS { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Tipo { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("LogAlerta")]
    public class Monitoramento
    {
        [Key]
        public int CodLogAlerta { get; set; }
        public string Servidor { get; set; }
        public string Item { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public double? EspacoEmGb { get; set; }
        public double? TamanhoEmGb { get; set; }
        public string Disco { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}

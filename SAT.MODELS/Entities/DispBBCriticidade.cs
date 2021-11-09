using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DispBBCriticidade
    {
        [Key]
        public int CodDispBBCriticidade { get; set; }
        public string Detalhes { get; set; }
        public TimeSpan? HorarioInicio { get; set; }
        public TimeSpan? HorarioFim { get; set; }
        public bool? IndSeg { get; set; }
        public bool? IndTer { get; set; }
        public bool? IndQua { get; set; }
        public bool? IndQui { get; set; }
        public bool? IndSex { get; set; }
        public bool? IndSab { get; set; }
        public bool? IndDom { get; set; }
        public bool? IndFeriado { get; set; }
        public int IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public string NomeDispBBCriticidade { get; set; }
        public int? QTDDias { get; set; }
        public double? SLAVirtual { get; set; }
        public int? CodSLA { get; set; }
    }
}
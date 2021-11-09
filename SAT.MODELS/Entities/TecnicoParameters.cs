using System;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class TecnicoParameters : QueryStringParameters
    {
        public int? CodTecnico { get; set; }
        public string CodTecnicos { get; set; }
        public string Nome { get; set; }
        public int? IndAtivo { get; set; }
        public string CodFiliais { get; set; }
        public int? IndFerias { get; set; }
        public string CodStatusServicos { get; set; }
        public int? CodPerfil { get; set; }
        public int? CodAutorizada { get; set; }
        public int? PA { get; set; }
        public DateTime PeriodoMediaAtendInicio { get; set; }
        public DateTime PeriodoMediaAtendFim { get; set; }
        public TecnicoIncludeEnum Include { get; set; }
        public TecnicoFilterEnum FilterType { get; set; }
    }
}

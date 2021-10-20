using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class TecnicoParameters : QueryStringParameters
    {
        public int? CodTecnico { get; set; }
        public string Nome { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodFilial { get; set; }
        public string CodFiliais { get; set; }
        public int? IndFerias { get; set; }
        public string CodStatusServicos { get; set; }
        public int? CodPerfil { get; set; }
        public int? CodAutorizada { get; set; }
        public int? PA { get; set; }
        public DateTime PeriodoMediaAtendInicio { get; set; }
        public DateTime PeriodoMediaAtendFim { get; set; }
    }
}

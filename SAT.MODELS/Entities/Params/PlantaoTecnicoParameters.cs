using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PlantaoTecnicoParameters : QueryStringParameters
    {
        public int? CodTecnico { get; set; }
        public byte? IndAtivo { get; set; }
        public string Nome { get; set; }
        public DateTime? DataPlantaoInicio { get; set; }
        public DateTime? DataPlantaoFim { get; set; }
    }
}

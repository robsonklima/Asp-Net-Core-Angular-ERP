using System;

namespace SAT.MODELS.Entities
{
    public class PlantaoTecnicoRegiao
    {
        public int CodPlantaoTecnicoRegiao { get; set; }
        public int CodPlantaoTecnico { get; set; }
        public int CodRegiao { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public Regiao Regiao { get; set; }
    }
}
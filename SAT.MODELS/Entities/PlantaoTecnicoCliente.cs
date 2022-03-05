using System;

namespace SAT.MODELS.Entities
{
    public class PlantaoTecnicoCliente
    {
        public int CodPlantaoTecnicoCliente { get; set; }
        public int CodPlantaoTecnico { get; set; }
        public int CodCliente { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public Cliente Cliente { get; set; }
    }
}
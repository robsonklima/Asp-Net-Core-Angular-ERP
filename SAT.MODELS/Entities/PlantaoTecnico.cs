using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class PlantaoTecnico
    {
        public int CodPlantaoTecnico { get; set; }
        public int CodTecnico { get; set; }
        public DateTime DataPlantao { get; set; }
        public byte IndAtivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public Tecnico Tecnico { get; set; }
        public List<PlantaoTecnicoRegiao> PlantaoRegioes { get; set; }
        public List<PlantaoTecnicoCliente> PlantaoClientes { get; set; }
    }
}

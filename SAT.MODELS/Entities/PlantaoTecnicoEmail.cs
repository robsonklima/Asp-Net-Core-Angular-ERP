using System;

namespace SAT.MODELS.Entities
{
    public class PlantaoTecnicoEmailDetalhado
    {
        public string Filial { get; set; }
        public string Tecnico { get; set; }
        public string Matricula { get; set; }
        public string Regiao { get; set; }
        public string Data { get; set; }
        public string Dia { get; set; }
    }

    public class PlantaoTecnicoEmailResumido
    {
        public string Filial { get; set; }
        public int Qtd { get; set; }
    }
}
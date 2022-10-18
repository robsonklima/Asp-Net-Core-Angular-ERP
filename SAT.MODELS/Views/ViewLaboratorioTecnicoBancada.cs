namespace SAT.MODELS.Views {
    public class ViewLaboratorioTecnicoBancada
    {
        public string Nome { get; set; }
        public string CodUsuario { get; set; }
        public string EmReparo { get; set; }
        public string TempoOcioso { get; set; }
        public string TempoEmReparo { get; set; }
        public int QtdEmReparo { get; set; }
        public int NumBancada { get; set; }
        public string TempoReparoPeca { get; set; }
        public string StatusReparo { get; set; }
        public int? CodOR { get; set; }
    }
}
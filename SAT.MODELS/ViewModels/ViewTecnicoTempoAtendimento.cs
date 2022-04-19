namespace SAT.MODELS.ViewModels {
    public class ViewTecnicoTempoAtendimento
    {
        public int CodTecnico { get; set; }
        public string Nome { get; set; }
        public int CodTipoIntervencao { get; set; }
        public string NomTipoIntervencao { get; set; }
        public int TempoEmMinutos { get; set; }
        public int CodEquip { get; set; }
    }
}
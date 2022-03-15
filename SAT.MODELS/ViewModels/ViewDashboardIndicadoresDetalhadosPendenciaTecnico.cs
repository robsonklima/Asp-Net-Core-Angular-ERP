namespace SAT.MODELS.ViewModels
{
    public class ViewDashboardIndicadoresDetalhadosPendenciaTecnico
    {
       public int CodFilial { get; set; }
        public string Filial { get; set; }
        public string NomeTecnico { get; set; }
        public int ChamadosMes { get; set; }
        public int ChamadosMesPecasPendentes { get; set; }
        public int TotalGeral { get; set; }
        public decimal Percentual { get; set; }
    }
}

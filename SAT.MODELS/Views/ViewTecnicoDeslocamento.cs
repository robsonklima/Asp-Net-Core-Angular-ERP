using System;

namespace SAT.MODELS.Views
{
    public class ViewTecnicoDeslocamento
    {
        public int CodOS { get; set; }
        public int CodStatusServico { get; set; }
        public int CodFilial { get; set; }
        public int PA { get; set; }
        public string Tecnico { get; set; }
        public string Filial { get; set; }
        public string LocalLat { get; set; }
        public string LocalLng { get; set; }
        public DateTime? Transf { get; set; }
        public DateTime? Leitura { get; set; }
        public DateTime? Intencao { get; set; }
        public double? IntencaoLat { get; set; }
        public double? IntencaoLng { get; set; }
        public DateTime? Checkin { get; set; }
        public string CheckinLat { get; set; }
        public string CheckinLng { get; set; }
        public DateTime? Checkout { get; set; }
        public string CheckoutLat { get; set; }
        public string CheckoutLng { get; set; }
        public int CodRegiao { get; set; }
        public int CodAutorizada { get; set; }
    }
}


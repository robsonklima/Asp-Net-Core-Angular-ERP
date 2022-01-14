using System;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class Deslocamento
    {
        public DeslocamentoOrigem Origem { get; set; }
        public DeslocamentoDestino Destino { get; set; }
        public double? Distancia { get; set; }
        public double? Tempo { get; set; }
        public double? TempoCheckin { get; set; }
        public DeslocamentoTipoEnum? Tipo { get; set; }
        public DateTime? Data { get; set; }
    }

    public class DeslocamentoOrigem
    {
        public string Descricao { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }

    public class DeslocamentoDestino
    {
        public string Descricao { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
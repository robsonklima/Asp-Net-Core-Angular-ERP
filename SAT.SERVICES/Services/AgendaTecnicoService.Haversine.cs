using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System;
using System.Globalization;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        private double DistanciaEmMinutos(OrdemServico osAtual, OrdemServico osAnterior)
        {
            double? orig_lat = null, orig_long = null, dest_lat = null, dest_long = null;

            if (osAnterior != null)
            {
                if (!string.IsNullOrEmpty(osAnterior.LocalAtendimento?.Latitude)) 
                    orig_lat = double.Parse(osAnterior.LocalAtendimento.Latitude, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(osAnterior.LocalAtendimento?.Longitude)) 
                    orig_long = double.Parse(osAnterior.LocalAtendimento.Longitude, CultureInfo.InvariantCulture);
            }
            else if (osAtual.Tecnico != null)
            {
                if (!string.IsNullOrEmpty(osAtual.Tecnico?.Latitude)) 
                    orig_lat = double.Parse(osAtual.Tecnico.Latitude, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(osAtual.Tecnico?.Longitude)) 
                    orig_long = double.Parse(osAtual.Tecnico.Longitude, CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(osAtual.LocalAtendimento?.Latitude)) 
                dest_lat = double.Parse(osAtual.LocalAtendimento.Latitude, CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(osAtual.LocalAtendimento?.Longitude)) 
                dest_long = double.Parse(osAtual.LocalAtendimento.Longitude, CultureInfo.InvariantCulture);

            return this.GetDistanceInMinutesPerKm(orig_lat, orig_long, dest_lat, dest_long, 50);
        }

        public double GetDistanceInMinutesPerKm(double? origem_lat, double? origem_long, double? destino_lat, double? destino_long, int velocidadeMediaEmHoras)
        {
            // Se todas as coordenadas estiverem disponiveis, calcula a distância e retorna o tempo em minutos
            if
            (
                (origem_lat.HasValue && origem_lat.Value != 0) && 
                (origem_long.HasValue && origem_long.Value != 0) && 
                (destino_lat.HasValue && destino_lat.Value != 0) && 
                (destino_long.HasValue && destino_long.Value != 0)
            )
            {
                var distance = this.CalculateDistanceInKm(origem_lat.Value, origem_long.Value, destino_lat.Value, destino_long.Value);
                var hours = ((distance / velocidadeMediaEmHoras));
                return hours * 60;
            }

            return 30;
        }

        private double CalculateDistanceInKm(double orig_lat, double orig_long, double dest_lat, double dest_long)
        {
            double R = 6376.5000;
            var lat1 = ToRad(orig_lat);
            var lat2 = ToRad(dest_lat);
            var lon1 = ToRad(orig_long);
            var lon2 = ToRad(dest_long);
            var dLat = lat2 - lat1;
            var dLon = lon2 - lon1;
            var a = Math.Pow(Math.Sin(dLat / 2), 2) + (Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2));
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;

            return distance;
        }

        public double ToRad(double degs)
        {
            return degs * (Math.PI / 180.0);
        }
    }
}
using SAT.INFRA.Interfaces;
using SAT.SERVICES.Interfaces;
using System;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public double GetDistanceInMinutesPerKm(double? origem_lat, double? origem_long, double? destino_lat, double? destino_long, int velocidadeMediaEmHoras)
        {
            // Se todas as coordenadas estiverem disponiveis, calcula a distância e retorna o tempo em minutos
            if (origem_lat.HasValue && origem_long.HasValue && destino_lat.HasValue && destino_long.HasValue)
            {
                var distance = this.CalculateDistanceInKm(origem_lat.Value, origem_long.Value, destino_lat.Value, destino_long.Value);
                var hours = ((distance / velocidadeMediaEmHoras));
                return hours * 60;
            }

            // Se não, retorna um tempo padrão de deslocamento
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
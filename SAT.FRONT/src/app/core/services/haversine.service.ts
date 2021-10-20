import { Injectable } from '@angular/core';
import { Coordenada } from '../types/agenda-tecnico.types';

@Injectable({
  providedIn: 'root'
})

export class HaversineService 
{
  constructor() { }

  private calculateDistanceInKm(orig_lat: number, orig_long: number, dest_lat: number, dest_long: number) 
  {
    var p = 0.017453292519943295;  // Math.PI / 180
    var c = Math.cos;
    var a = 0.5 - c((dest_lat - orig_lat) * p)/2 + 
            c(orig_lat * p) * c(dest_lat * p) * 
            (1 - c((dest_long - orig_long) * p))/2;

    return 12742 * Math.asin(Math.sqrt(a)); // 2 * R; R = 6371 km
  }

  public getDistanceInMinutesPerKm(origem: Coordenada, destino: Coordenada, velocidadeMediaEmHoras: number)
  {
    // Se todas as coordenadas estiverem disponiveis, calcula a distância e retorna o tempo em minutos
    if (origem.cordenadas[0] && origem.cordenadas[1] && destino.cordenadas[0] && destino.cordenadas[1])
      return ((this.calculateDistanceInKm(parseFloat(origem.cordenadas[0]), 
              parseFloat(origem.cordenadas[1]), 
              parseFloat(destino.cordenadas[0]), 
              parseFloat(destino.cordenadas[1])) / velocidadeMediaEmHoras) * 60.0);

      // Se não, retorna um tempo padrão de deslocamento
      return 30;
  }
}
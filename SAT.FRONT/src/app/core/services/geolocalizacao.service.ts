import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from '../config/app.config'
import { Geolocalizacao, GeolocalizacaoParameters, GeolocalizacaoServiceEnum } from '../types/geolocalizacao.types';

@Injectable({
  providedIn: 'root'
})
export class GeolocalizacaoService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: GeolocalizacaoParameters): Observable<Geolocalizacao>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Geolocalizacao`, { params: params }).pipe(
      map((data: Geolocalizacao) => data)
    )
  }

  obterDistancia(parameters: GeolocalizacaoParameters): Observable<Geolocalizacao>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/Geolocalizacao/DistanceMatrix`, { params: params }).pipe(
      map((data: Geolocalizacao) => data)
    )
  }
}
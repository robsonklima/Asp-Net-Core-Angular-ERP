import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from '../../core/config/app.config'
import { GoogleGeolocation, GoogleGeolocationParameters } from '../types/google-geolocation.types';

@Injectable({
  providedIn: 'root'
})
export class GoogleGeolocationService
{
  constructor (
    private http: HttpClient
  ) { }

  obterPorParametros(parameters: GoogleGeolocationParameters): Observable<GoogleGeolocation>
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/GoogleGeolocation`, { params: params }).pipe(
      map((data: GoogleGeolocation) => data)
    )
  }

  public async obterPorEndereco(cep: string)
  {
    if (cep == null) return null;
    return (await this.obterPorParametros({ enderecoCep: cep.trim(), pageSize: 1 }))
      .toPromise()
      .then(result => { return result.results.shift() });
  }

  calcularDistancia(originLat: string, originLong: string, destinationLat: string, destinationLong: string): Observable<any>
  {
    var key = 'AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM';

    originLat = encodeURIComponent(originLat);
    originLong = encodeURIComponent(originLong);
    destinationLat = encodeURIComponent(destinationLat);
    destinationLong = encodeURIComponent(destinationLong);

    const url = `https://maps.googleapis.com/maps/api/distancematrix/json?destinations=${destinationLat}%2C${destinationLong}&origins=${originLat}%2C${originLong}&key=${key}`;

    return this.http.get<any>(url).pipe(map((data: any) => data));
  }
}
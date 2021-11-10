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

  obterDistancia(parameters: GoogleGeolocationParameters): any
  {
    let params = new HttpParams();

    Object.keys(parameters).forEach(key =>
    {
      if (parameters[key] !== undefined && parameters[key] !== null) params = params.append(key, String(parameters[key]));
    });

    return this.http.get(`${c.api}/GoogleGeolocation/DistanceMatrix`, { params: params }).pipe(
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

}
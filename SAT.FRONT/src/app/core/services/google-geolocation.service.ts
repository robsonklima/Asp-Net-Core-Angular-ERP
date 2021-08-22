import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from '../../core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class GoogleGeolocationService {
  constructor(
    private http: HttpClient
  ) {}

  buscarPorEnderecoOuCEP(endCep: string): Observable<any> {
    const url = `https://maps.googleapis.com/maps/api/geocode/json?address=
      ${endCep}&key=${c.google_key}&libraries=places&callback=initMap"`;
      
    return this.http.get<any>(url).pipe(
      map((obj) => obj)
    );
  }
}
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from '../../core/config/app.config'
import { GoogleGeolocation } from '../types/google-geolocation.types';

@Injectable({
  providedIn: 'root'
})
export class GoogleGeolocationService {
  constructor(
    private http: HttpClient
  ) {}

  buscarPorEnderecoOuCEP(endCep: string): Observable<any> {
    const url = `${c.api}/GoogleGeolocation/${endCep}`;

    return this.http.get<GoogleGeolocation>(url).pipe(
      map((obj) => obj)
    );
  }
}
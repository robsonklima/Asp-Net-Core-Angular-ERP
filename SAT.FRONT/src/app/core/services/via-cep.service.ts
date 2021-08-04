// Documentation: https://viacep.com.br/

import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";


@Injectable({
  providedIn: 'root'
})
export class ViaCepService {
  constructor(
    private http: HttpClient
  ) {}

  buscarPorCep(cep: string): Observable<any> {
    const url = `https://viacep.com.br/ws/${cep}/json/`;
    return this.http.get<any>(url).pipe(
      map((obj) => obj)
    );
  }

  errorHandler(e: any): Observable<any> {
    return EMPTY;
  }
}

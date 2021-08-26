import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class IndicadorService {
  constructor(private http: HttpClient) {}

  obter(): Observable<any> {
    return this.http.get(`${c.api}/Indicador`).pipe(
      map((data: any) => data)
    )
  }
}

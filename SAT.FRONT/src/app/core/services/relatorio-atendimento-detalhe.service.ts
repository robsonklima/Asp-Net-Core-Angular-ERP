import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { appConfig as c } from 'app/core/config/app.config'

@Injectable({
  providedIn: 'root'
})
export class RelatorioAtendimentoDetalheService {
  constructor(private http: HttpClient) {}

  deletar(codRATDetalhe: number): Observable<void> {
    const url = `${c.api}/RelatorioAtendimentoDetalhe/${codRATDetalhe}`;

    return this.http.delete<void>(url).pipe(
      map((obj) => obj)
    );
  }
}

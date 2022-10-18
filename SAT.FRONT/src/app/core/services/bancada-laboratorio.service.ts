import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { appConfig as c } from 'app/core/config/app.config'
import { ViewLaboratorioTecnicoBancada } from '../types/bancada-laboratorio.types';
import { map } from "rxjs/operators";

@Injectable({
  providedIn: 'root'
})
export class BancadaLaboratorioService {
  constructor(
    private http: HttpClient
  ) { }

  obterTecnicosBancada(): Observable<ViewLaboratorioTecnicoBancada[]> {
    const url = `${c.api}/BancadaLaboratorio/TecnicosBancada`;

    return this.http.get<ViewLaboratorioTecnicoBancada[]>(url).pipe(
      map((obj) => obj)
    );
  }
}

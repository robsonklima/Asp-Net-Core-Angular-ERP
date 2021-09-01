import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { Config } from '../models/config';

import { Observable } from "rxjs/Observable";
import { Auditoria } from '../models/auditoria';

@Injectable()
export class AuditoriaService {
  constructor(
    private http: Http
  ) {}

  buscarAuditoriasPorUsuario(codUsuario: string): Observable<Auditoria[]> {
    return this.http.get(Config.API_URL + 'Auditoria/' + codUsuario + '/')
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  enviarAuditoriaApi(auditoria: Auditoria): Observable<Auditoria> {
    return this.http.post(Config.API_URL + 'Auditoria', auditoria)
      .map((res: Response) => {return res.json()})
      .catch((error: any) => {return Observable.throw(error)});
  }
}
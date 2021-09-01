import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Laudo } from '../models/laudo';

@Injectable()
export class LaudoService {
  constructor(
    private http: Http
  ) { }

  buscarLaudosApi(codTecnico: number): Observable<Laudo[]> {
    return this.http.get(Config.API_URL + 'Laudo/' + codTecnico)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json())
    );
  }

  enviarLaudoApi(laudo: Laudo): Observable<any> {
    return this.http.post(Config.API_URL + 'Laudo', laudo)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }
}
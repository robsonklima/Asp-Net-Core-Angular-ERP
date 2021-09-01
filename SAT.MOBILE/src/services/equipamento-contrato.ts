import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { Chamado } from "../models/chamado";

@Injectable()
export class EquipamentoContratoService {
  constructor(
    private http: Http
  ) { }

  buscarEquipamentoContratoHistOs(codEquipContrato: number): Observable<Chamado[]> {
    return this.http.get(Config.API_URL + 'EquipamentoHistOs/' + codEquipContrato)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }
}
import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Observable } from "rxjs/Observable";
import { AcaoCausa } from '../models/acao-causa';
import { Config } from '../models/config';


@Injectable()
export class AcaoCausaService {
  private acoesCausas: AcaoCausa[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarAcoesCausasApi(): Observable<AcaoCausa[]> {
    return this.http.get(Config.API_URL + 'AcaoCausa')
      .timeout(120000)
      .map((res: Response) => { this.storage.set('AcoesCausas', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarAcoesCausasStorage() {
    return this.storage.get('AcoesCausas').then((dCausas: AcaoCausa[]) => {
      this.acoesCausas = dCausas != null ? dCausas : [];
      return this.acoesCausas.slice();
    })
    .catch();
  }

  buscarAcoesPorCausa(codECausa: string): Promise<AcaoCausa[]> {
    return new Promise((resolve, reject) => {
      this.buscarAcoesCausasStorage().then((aCausas: AcaoCausa[]) => { 
        let acoesCausas: AcaoCausa[] = aCausas.filter(acoesCausas => {
          return acoesCausas.causa.codECausa === codECausa;
        });
        
        resolve(acoesCausas);
      }).catch(() => {
        reject();
      });
    });
  }
}
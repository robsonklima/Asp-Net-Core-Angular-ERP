import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";

import 'rxjs/Rx';

import { Observable } from "rxjs/Observable";
import { DefeitoCausa } from '../models/defeito-causa';
import { Config } from '../models/config';


@Injectable()
export class DefeitoCausaService {
  private defeitosCausas: DefeitoCausa[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarDefeitosCausasApi(): Observable<DefeitoCausa[]> {
    return this.http.get(Config.API_URL + 'DefeitoCausa')
      .timeout(180000)
      .map((res: Response) => { this.storage.set('DefeitosCausas', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarDefeitosCausasStorage(): Promise<DefeitoCausa[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('DefeitosCausas').then((dCausas: DefeitoCausa[]) => {
        this.defeitosCausas = dCausas != null ? dCausas : [];

        resolve(this.defeitosCausas.slice());
      }).catch(() => reject());
    });
  }

  buscarDefeitosPorCausa(codECausa: string): Promise<DefeitoCausa[]> {
    return new Promise((resolve, reject) => {
      this.buscarDefeitosCausasStorage().then((dCausas: DefeitoCausa[]) => { 
        let defeitosCausas: DefeitoCausa[] = dCausas.filter(defeitosCausas => {
          return defeitosCausas.causa.codECausa === codECausa;
        });
        
        resolve(defeitosCausas);
      }).catch(() => reject());
    });
  }
}
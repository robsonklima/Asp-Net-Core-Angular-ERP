import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { EquipamentoPOS } from '../models/equipamentoPOS';


@Injectable()
export class EquipamentoPOSService {
  private equipamentos: EquipamentoPOS[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarEquipamentosPOSApi(): Observable<EquipamentoPOS[]> {
    return this.http.get(Config.API_URL + 'EquipamentoPOS')
      .timeout(15000)
      .map((res: Response) => { this.storage.set('EquipamentosPOS', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarEquipamentosPOSStorage(): Promise<EquipamentoPOS[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('EquipamentosPOS').then((equipamentos: EquipamentoPOS[]) => {
        this.equipamentos = equipamentos != null ? equipamentos : [];
        resolve(this.equipamentos.slice());
      }).catch(() => {
        reject();
      });
    });
  }
}
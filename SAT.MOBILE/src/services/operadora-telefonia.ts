import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { OperadoraTelefonia } from '../models/operadora-telefonia';


@Injectable()
export class OperadoraTelefoniaService {
  private operadoras: OperadoraTelefonia[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarOperadorasApi(): Observable<OperadoraTelefonia[]> {
    return this.http.get(Config.API_URL + 'OperadoraTelefonia')
      .map((res: Response) => { this.storage.set('OperadorasTelefonia', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarOperadorasStorage(): Promise<OperadoraTelefonia[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('OperadorasTelefonia').then((operadoras: OperadoraTelefonia[]) => {
        this.operadoras = operadoras != null ? operadoras : [];
        resolve(this.operadoras.slice());
      }).catch(() => {
        reject();
      });
    });
  }
}
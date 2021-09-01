import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { FerramentaTecnico } from '../models/ferramenta-tecnico';


@Injectable()
export class FerramentaTecnicoService {
  private ferramentas: FerramentaTecnico[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarFerramentasTecnicoApi(codUsuario: string, ferramentas: FerramentaTecnico[]): Observable<any> {
    return this.http.post(Config.API_URL + 'FerramentaTecnico', { codUsuario: codUsuario, ferramentasTecnico: ferramentas })
      .map((res: Response) => { this.storage.set('FerramentasTecnico', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarFerramentasTecnicoStorage(): Promise<FerramentaTecnico[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('FerramentasTecnico').then((ferramentas: FerramentaTecnico[]) => {
        this.ferramentas = ferramentas != null ? ferramentas : [];
        resolve(this.ferramentas.slice());
      }).catch(() => reject());
    });
  }

  atualizarFerramentaTecnico(ferramentaTecnico: FerramentaTecnico): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.storage.get('FerramentasTecnico').then((ferramentas: FerramentaTecnico[]) => {
        ferramentas.forEach((f, i) => {
          if(f.codFerramentaTecnico === ferramentaTecnico.codFerramentaTecnico)
            ferramentas[i] = ferramentaTecnico;
        });

        this.storage.set('FerramentasTecnico', ferramentas).then(() => { resolve(true) }).catch(() => { reject(false) });
      }).catch(() => reject(false));
    });
  }
}
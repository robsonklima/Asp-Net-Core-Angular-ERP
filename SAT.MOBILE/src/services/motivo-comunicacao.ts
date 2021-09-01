import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { MotivoComunicacao } from '../models/motivo-comunicacao';


@Injectable()
export class MotivoComunicacaoService {
  private motivos: MotivoComunicacao[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarMotivosComunicacaoPOSApi(): Observable<MotivoComunicacao[]> {
    return this.http.get(Config.API_URL + 'MotivoComunicacao')
      .map((res: Response) => { this.storage.set('MotivosComunicacao', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarMotivosComunicacaoStorage(): Promise<MotivoComunicacao[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('MotivosComunicacao').then((motivos: MotivoComunicacao[]) => {
        this.motivos = motivos != null ? motivos : [];
        resolve(this.motivos.slice());
      }).catch(() => {
        reject();
      });
    });
  }
}
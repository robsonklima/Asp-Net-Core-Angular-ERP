import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { MotivoCancelamento } from '../models/motivo-cancelamento';


@Injectable()
export class MotivoCancelamentoService {
  private motivos: MotivoCancelamento[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarMotivosCancelamentoPOSApi(): Observable<MotivoCancelamento[]> {
    return this.http.get(Config.API_URL + 'MotivoCancelamento')
      .map((res: Response) => { this.storage.set('MotivosCancelamento', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarMotivosCancelamentoStorage(): Promise<MotivoCancelamento[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('MotivosCancelamento').then((motivos: MotivoCancelamento[]) => {
        this.motivos = motivos != null ? motivos : [];
        resolve(this.motivos.slice());
      }).catch(() => {
        reject();
      });
    });
  }
}
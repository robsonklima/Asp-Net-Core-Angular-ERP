import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';

import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { DefeitoPOS } from '../models/defeito-pos';


@Injectable()
export class DefeitoPOSService {
  private defeitos: DefeitoPOS[] = [];

  constructor(
    private http: Http,
    private storage: Storage
  ) { }

  buscarDefeitosPOSApi(): Observable<DefeitoPOS[]> {
    return this.http.get(Config.API_URL + 'DefeitoPOS')
      .map((res: Response) => { this.storage.set('DefeitosPOS', res.json()).catch() })
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarDefeitosPOSStorage(): Promise<DefeitoPOS[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('DefeitosPOS').then((defeitos: DefeitoPOS[]) => {
        this.defeitos = defeitos != null ? defeitos : [];
        
        resolve(this.defeitos.slice());
      }).catch(() => reject());
    });
  }
}
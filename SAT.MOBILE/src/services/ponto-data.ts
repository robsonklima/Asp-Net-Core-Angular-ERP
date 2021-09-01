import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Storage } from "@ionic/storage";
import 'rxjs/Rx';
import { Config } from '../models/config';
import { Observable } from "rxjs/Observable";
import { PontoData } from '../models/ponto-data';


@Injectable()
export class PontoDataService {
  pontosData: PontoData[] = [];
  storageName: string = 'PontosData';

  constructor(
    private http: Http,
    private storage: Storage
  ) {}

  buscarPontosDataStorage() {
    return this.storage.get(this.storageName)
      .then((pontosData: PontoData[]) => {
        this.pontosData = pontosData != null ? pontosData : [];

        return this.pontosData.slice();
      })
      .catch();
  }

  atualizarPontosDataStorage(pontosData: PontoData[]) {
    this.storage.set(this.storageName, pontosData).catch()
  }

  atualizarPontoDataStorage(PontoData: PontoData) {

  }

  buscarPontosDataPorUsuarioApi(codUsuario: string): Observable<PontoData[]> {
    return this.http.get(Config.API_URL + 'PontoData/' + codUsuario)
      .timeout(30000)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error));
  }

  enviarPontoDataApi(pontosData: PontoData[], codUsuario: string): Observable<PontoData[]> {
    return this.http.post(Config.API_URL + 'PontoData', {pontosData: pontosData, codUsuario: codUsuario})
      .timeout(30000)
      .map((res: Response) => { return res.json()})
      .catch((error: any) => {return Observable.throw(error)});
  }
}
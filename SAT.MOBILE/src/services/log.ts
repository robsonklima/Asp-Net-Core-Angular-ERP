import { Injectable } from '@angular/core';
import { Storage } from '@ionic/storage';
import { Http, Response } from '@angular/http';

import { Log } from '../models/log';
import moment from 'moment';
import { Config } from '../models/config';
import { Observable } from 'rxjs';
import { DadosGlobaisService } from './dados-globais';


@Injectable()
export class LogService {
  logs: Log[] = [];

  constructor(
    private storage: Storage,
    private http: Http,
    private dgService: DadosGlobaisService
  ) {}

  buscarLogs(): Promise<Log[]> {
    return new Promise((resolve, reject) => {
      this.storage.get('Logs').then((logs: Log[]) => {
        this.logs = logs != null ? logs
          .sort((a, b) => { return ((a.dataHoraCad > b.dataHoraCad) ? -1 : ((a.dataHoraCad < b.dataHoraCad) ? 1 : 0))}) : [];

        this.atualizarLogs(this.logs);
        resolve (this.logs);
      })
      .catch(() => {
        reject();
      });
    });
  }

  adicionarLog(tipo: string, mensagem: string) {
    this.dgService.buscarDadosGlobaisStorage().then((dg) => {
      let log = new Log();
      log.tipo = tipo;
      log.dataHoraCad = moment().format('DD/MM HH:mm:ss');
      log.codUsuarioCad = dg.usuario.codUsuario;
      log.mensagem = mensagem || 'Não Informado';
      log.versaoApp = Config.VERSAO_APP;

      this.buscarLogs().then(logs => {
        this.logs = logs;
        this.logs.push(log);
        this.storage.set('Logs', this.logs).catch();
      }).catch();
    }).catch();
  }

  apagarLogs(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.logs = [];

      this.storage.set('Logs', this.logs)
        .then(() => {
          resolve(true);
        })
        .catch(() => {
          reject(false);
        });
    });
  }

  enviarLogsApi(): Observable<any> {
    return this.http.post(Config.API_URL + 'Log', this.logs)
      .map((res: Response) => {
        return res.json()
      })
      .catch((error: any) => {
        return Observable.throw(error);
      });
  }

  atualizarLogs(logs: Log[]) {
    this.logs = logs.slice(0, 50);
    this.storage.set('Logs', logs).catch();
  }
}
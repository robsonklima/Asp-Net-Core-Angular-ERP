import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Platform, AlertController } from 'ionic-angular';

import { Config } from '../models/config';
import { Localizacao } from '../models/localizacao';

import { Observable } from "rxjs/Observable";
import { Diagnostic } from '@ionic-native/diagnostic';
import { LoadingFactory } from '../factories/loading-factory';
import { Geolocation } from '@ionic-native/geolocation';
import { Cep } from '../models/cep';


@Injectable()
export class GeolocationService {
  localizacao: Localizacao = new Localizacao();

  constructor(
    private http: Http,
    private platform: Platform,
    private diagnostic: Diagnostic,
    private geolocation: Geolocation,
    private loadingFactory: LoadingFactory,
    private alertCtrl: AlertController
  ) {}


  buscarUltimaLocalizacao(): Localizacao {
    return this.localizacao;
  }

  buscarMinhaLocalizacao(verbose: boolean=false): Promise<Localizacao> {
    return new Promise((resolve, reject) => {
      this.platform.ready().then(() => {
        if(verbose) this.loadingFactory.exibir(Config.MSG.OBTENDO_LOCALIZACAO);
        
        this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((location) => {
          if(verbose) this.loadingFactory.encerrar();
          this.localizacao.dataHoraCad = new Date().toLocaleString();
          this.localizacao.latitude = location.coords.latitude;
          this.localizacao.longitude = location.coords.longitude;
          resolve(this.localizacao);
        }).catch(() => { 
          if(verbose) this.loadingFactory.encerrar();
          reject();
        });
      }).catch(() => {
        reject();
      });
    });
  }

  atualizarMinhaLocalizacao(localizacao: Localizacao): Promise<Localizacao> {
    return new Promise((resolve, reject) => {
      if (localizacao.latitude && localizacao.longitude) {
        this.localizacao = localizacao;

        resolve(localizacao);
      } else {
        reject();
      }
    });
  }

  buscarDetalhesPorEnderecoApi(endereco: string): Observable<any> {
    return this.http.get(`https://nominatim.openstreetmap.org/search?q=${endereco}&format=json&polygon=1&addressdetails=1=`)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarDetalhesPorCepApi(cep: string): Observable<Cep> {
    cep = cep.replace(/\D/g, '');

    return this.http.get(`https://viacep.com.br/ws/${cep}/json`)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  buscarDetalhesLocalPorCoordenadasApi(localizacao: Localizacao): Observable<any> {
    return this.http.get(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${localizacao.latitude}&lon=${localizacao.longitude}&addressdetails=1`)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  enviarLocalizacao(localizacao: Localizacao): Observable<any> {
    return this.http.post(Config.API_URL + 'LocalizacaoTecnico', localizacao)
      .timeout(20000)
      .map((res: Response) => res.json())
      .catch((error: any) => Observable.throw(error.json()));
  }

  verificarSeGPSEstaAtivoEDirecionarParaConfiguracoes() {
    if (!this.platform.is('cordova')) return;

    this.diagnostic.isLocationEnabled().then((isEnabled) => {
      if(!isEnabled){
        const confirmacao = this.alertCtrl.create({
          title: Config.MSG.GPS_DESATIVADO,
          message: Config.MSG.GPS_DESATIVADO_ORIENTACAO,
          buttons: [
            {
              text: Config.MSG.OK,
              handler: () => {
                this.diagnostic.switchToLocationSettings();
              }
            }
          ]
        });
    
        confirmacao.present();
      }
    })
  }
}
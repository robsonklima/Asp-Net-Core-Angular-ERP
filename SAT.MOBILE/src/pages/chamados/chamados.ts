import { Component } from '@angular/core';
import { NavController, AlertController, Events, ToastController, Platform } from 'ionic-angular';

import { Badge } from '@ionic-native/badge';
import { LaunchNavigator } from '@ionic-native/launch-navigator';

import moment from 'moment';

import { DadosGlobais } from '../../models/dados-globais';
import { Chamado } from '../../models/chamado';

import { DadosGlobaisService } from '../../services/dados-globais';
import { ChamadoService } from "../../services/chamado";

import { ChamadoPage } from "../chamados/chamado";
import { Config } from '../../models/config';
import { ChamadosFechadosPage } from './chamados-fechados';
import { ChamadoFechadoPage } from './chamado-fechado';
import { LoadingFactory } from '../../factories/loading-factory';
import { Geolocation } from '@ionic-native/geolocation';
import { GeolocationService } from '../../services/geo-location';
import { Localizacao } from '../../models/localizacao';


@Component({
  selector: 'chamados-page',
  templateUrl: 'chamados.html'
})
export class ChamadosPage {
  chamados: Chamado[];
  chamadosAbertos: Chamado[];
  chamadosFechados: Chamado[];
  qtdChamadosFechadosAExibir: Number = 20;
  dg: DadosGlobais;
  status: string = "abertos";

  constructor(
    private platform: Platform,
    private alertCtrl: AlertController,
    private navCtrl: NavController,
    private toastCtrl: ToastController,
    private launchNavigator: LaunchNavigator,
    private badge: Badge,
    private events: Events,
    private geolocation: Geolocation,
    private chamadoService: ChamadoService,
    private dadosGlobaisService: DadosGlobaisService,
    private geolocationService: GeolocationService,
    private loadingFactory: LoadingFactory
  ) {}

  
  ionViewWillEnter() {
    this.carregarDadosGlobais().then(() => {
      this.carregarChamadosStorage().then(() => {
        this.carregarChamadosFechadosApi();

        this.enviarLocalizacao();
      }).catch();
    });
   
    this.geolocationService.verificarSeGPSEstaAtivoEDirecionarParaConfiguracoes();
  }

  ngOnInit() {
    this.events.subscribe('sincronizacao:efetuada', () => {
      this.carregarChamadosStorage();
    });
  }

  public telaChamado(chamado: Chamado) {
    this.navCtrl.push(ChamadoPage, { chamado: chamado });
  }

  public abrirNavegacao(chamado: Chamado) {
    if (!this.platform.is('cordova'))
      this.exibirToast(Config.MSG.RECURSO_NATIVO, Config.TOAST.ERROR);  
    else if (!chamado.localAtendimento.localizacao.latitude || !chamado.localAtendimento.localizacao.longitude)
      this.exibirToast(Config.MSG.ERRO_LOCAL_NAO_POSSUI_COORDENADAS, Config.TOAST.ERROR);  
    else
      this.launchNavigator.navigate([ chamado.localAtendimento.localizacao.latitude, chamado.localAtendimento.localizacao.longitude ]).catch();
  }

  public telaChamadoFechado(chamado: Chamado) {
    this.navCtrl.push(ChamadoFechadoPage, { chamado: chamado });
  }

  public telaChamadosFechados() {
    this.navCtrl.push(ChamadosFechadosPage);
  }

  public pushAtualizarChamados(refresher) {
    this.sincronizarChamados(true).then(() => {
      if (refresher) refresher.complete();
    }).catch(() => {
      setTimeout(() => { if (refresher) refresher.complete() }, 2000);
    });    
  }

  public limparChamadosDispositivo() {
    const confirmacao = this.alertCtrl.create({
      title: Config.MSG.CONFIRMACAO,
      message: Config.MSG.REMOVER_OS_CHAMADOS,
      buttons: [
        {
          text: Config.MSG.CANCELAR,
          handler: () => {}
        },
        {
          text: Config.MSG.CONFIRMAR,
          handler: () => {
            this.chamadoService.apagarChamadosStorage().then((res) => {
              this.chamadosAbertos = [];
              this.carregarChamadosStorage();
            })
            .catch();
          }
        }
      ]
    });

    confirmacao.present();
  } 

  public sincronizarChamados(verbose: boolean=false): Promise<any> {
    return new Promise((resolve, reject) => {
      this.chamadoService.sincronizarChamados(verbose, this.dg.usuario.codTecnico).then((cs) => {
        this.carregarChamadosStorage().catch();
        resolve();
      }).catch(() => { 
        this.carregarChamadosStorage().catch();
        reject();
      });
    });
  }

  public informarIntencaoAtendimento(i: number) {
    if (this.chamadoService.verificarExisteCheckinEmOutroChamado()) {
      this.exibirToast(Config.MSG.CHECKIN_EM_ABERTO, Config.TOAST.ERROR)
      return
    }

    this.platform.ready().then(() => {
      this.loadingFactory.exibir(Config.MSG.OBTENDO_LOCALIZACAO);

      this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((c) => {
        this.loadingFactory.encerrar();

        this.chamados.forEach((c, i) => {
          this.chamados[i].indIntencaoAtendimento = false;
          this.chamados[i].dataHoraIntencaoAtendimento = null;
        });
    
        this.chamados[i].indIntencaoAtendimento = true;
        this.chamados[i].dataHoraIntencaoAtendimento = new Date().toLocaleString('pt-BR');
    
        this.exibirToast(`Em deslocamento para o chamado ${this.chamados[i].codOs}`, Config.TOAST.SUCCESS)
        
        this.chamadoService.atualizarChamadosStorage(this.chamados);

        this.geolocationService.atualizarMinhaLocalizacao({ 
          latitude: c.coords.latitude, longitude: c.coords.longitude,
          dataHoraCad: new Date().toLocaleString('pt-BR'),
          codUsuario: this.dg.usuario.codUsuario
        });
      }).catch(() => { 
        this.loadingFactory.encerrar();
        this.exibirToast(Config.MSG.ERRO_AO_OBTER_LOCALIZACAO);
      });
    }).catch(() => {
      this.exibirToast(Config.MSG.ERRO_AO_OBTER_LOCALIZACAO);
    });
  }

  public cancelarIntencaoAtendimento(i: number) {
    if (this.chamadoService.verificarExisteCheckinEmOutroChamado()) {
      this.exibirToast(Config.MSG.CHECKIN_EM_ABERTO, Config.TOAST.ERROR)
      return
    }

    this.chamados.forEach((c, i) => {
      this.chamados[i].indIntencaoAtendimento = false;
      this.chamados[i].dataHoraIntencaoAtendimento = null;
    });

    this.exibirToast(Config.MSG.INTENCAO_CANCELADA, Config.TOAST.INFO)
    this.chamadoService.atualizarChamadosStorage(this.chamados);
  }

  private carregarDadosGlobais(): Promise<DadosGlobais> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage()
        .then((dados) => {
          if (dados)
            this.dg = dados;
            resolve(dados);
        })
        .catch((err) => {
          reject(new Error(err.message))
        });
    });
  }

  private carregarChamadosStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.chamadoService.buscarChamadosStorage().then((chamados) => {
        this.chamados = chamados;
        this.chamadosAbertos = chamados.filter((c) => { return (!c.dataHoraFechamento) });
        
        this.atualizarBadge();
        resolve();
      }).catch();
    });
  }

  private carregarChamadosFechadosApi() {
    this.chamadoService.buscarChamadosFechadosApi(this.dg.usuario.codTecnico).subscribe((cs: Chamado[]) => {
      this.chamadosFechados = cs.sort(function(a, b) { 
        return (moment(a.dataHoraFechamento, 'YYYY-MM-DD HH:mm').isBefore(moment(b.dataHoraFechamento, 'YYYY-MM-DD HH:mm')) ? -1 : (moment(a.dataHoraFechamento, 'YYYY-MM-DD HH:mm').isAfter(moment(b.dataHoraFechamento, 'YYYY-MM-DD HH:mm')) ? 1 : 0));
      });
    },
    err => {});
  }

  private enviarLocalizacao() {
    this.platform.ready().then(() => {
      this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((location) => {
        let loc = new Localizacao();
        loc.latitude = location.coords.latitude;
        loc.longitude = location.coords.longitude;
        loc.codUsuario = this.dg.usuario.codUsuario;
        loc.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

        this.geolocationService.enviarLocalizacao(loc).subscribe(() => {}, e => {});
      }).catch();
    }).catch();
  }

  private atualizarBadge() {
    this.badge.set( this.chamados.filter((c) => { return (!c.dataHoraFechamento) }).length );
  }

  private exibirToast(mensagem: string, tipo: string='info', posicao: string=null) {
    const toast = this.toastCtrl.create({
      message: mensagem, 
      duration: Config.TOAST.DURACAO, 
      position: posicao || 'bottom', 
      cssClass: 'toast-' + tipo
    });
    
    toast.present();
  }
}
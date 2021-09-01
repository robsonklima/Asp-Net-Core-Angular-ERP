import { Component, ViewChild } from '@angular/core';
import { Platform, NavController, MenuController, Events, ToastController, Toast, App, AlertController } from 'ionic-angular';
import { BackgroundGeolocation, BackgroundGeolocationResponse, BackgroundGeolocationEvents } from '@ionic-native/background-geolocation';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import moment from 'moment';
import { Chamado } from '../models/chamado';
import { Localizacao } from '../models/localizacao';

import { HomePage } from '../pages/home/home';
import { TutorialPage } from '../pages/tutorial/tutorial';
import { UsuarioPage } from '../pages/usuario/usuario';
import { SenhaAlteracaoPage } from "../pages/senha-alteracao/senha-alteracao";

import { DadosGlobais } from '../models/dados-globais';
import { DadosGlobaisService } from '../services/dados-globais';
import { GeolocationService } from '../services/geo-location';
import { UsuarioService } from '../services/usuario';
import { ChamadoService } from "../services/chamado";
import { Config } from '../models/config';


@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  toast: Toast;
  tutorialPage = TutorialPage;
  homePage = HomePage;
  @ViewChild('nav') nav: NavController;
  dataHoraUltAtualizacao: Date = new Date();
  dadosGlobais: DadosGlobais;
  chamados: Chamado[];
  task: any;

  constructor(
    statusBar: StatusBar,
    splashScreen: SplashScreen,
    private platform: Platform,
    public app: App,
    private events: Events,
    private toastCtrl: ToastController,
    private alertCtrl: AlertController,
    private bGeolocation: BackgroundGeolocation,
    private dadosGlobaisService: DadosGlobaisService,
    private geolocation: GeolocationService,
    private usuarioService: UsuarioService,
    private menuCtrl: MenuController,
    private chamadoService: ChamadoService
  ) {
    platform.ready().then(() => {
      statusBar.backgroundColorByHexString('#244681');
      splashScreen.hide();

      window.addEventListener('offline', () => { // offline - online
        this.exibirToast(Config.MSG.INTERNET_OFFLINE, Config.TOAST.ERROR);
      });

      if (platform.is('cordova')) { this.iniciarColetaLocalizacaoSegundoPlano() }

      this.events.subscribe('sincronizacao:solicitada', () => {
        this.chamadoService.sincronizarChamados(false, this.dadosGlobais.usuario.codTecnico).catch();
      });

      this.events.subscribe('login:efetuado', (dg: DadosGlobais) => { this.dadosGlobais = dg });
      
      this.dadosGlobaisService.buscarDadosGlobaisStorage().then((dados) => {
        if (dados)
          this.dadosGlobais = dados;

          if (dados) {
            if (dados.usuario) {
              this.usuarioService.salvarCredenciais(dados.usuario);
              this.menuCtrl.enable(true);
              this.nav.setRoot(this.homePage);
            } else {
              this.nav.setRoot(this.tutorialPage);
            }
          } else {
            this.nav.setRoot(this.tutorialPage);
          }
      }).catch();

      this.platform.registerBackButtonAction(() => {
        let nav = this.app.getActiveNavs()[0];
        let activeView = nav.getActive();                
        
        if(activeView.name === 'HomePage') {
          if (nav.canGoBack()){
            nav.pop();
          } else {
            const alert = this.alertCtrl.create({
                title: 'Fechar o App',
                message: Config.MSG.SAIR_APLICATIVO,
                buttons: [{
                    text: 'Ok',
                    role: 'cancel',
                    handler: () => {}
                }]
            });

            alert.present();
          }
        } else {
          nav.pop();
        }
      });
    });
  }

  public telaUsuario() {
    this.menuCtrl.close().then(() => {
      this.nav.push(UsuarioPage);
    })
  }

  public telaSenhaAlteracao() {
    this.menuCtrl.close().then(() => {
      this.nav.push(SenhaAlteracaoPage);
    })
  }

  private iniciarColetaLocalizacaoSegundoPlano() {
    this.bGeolocation.configure(Config.MAURON_85_CONFIG).then(() => {
      this.bGeolocation.on(BackgroundGeolocationEvents.location).subscribe((res: BackgroundGeolocationResponse) => {
        this.dadosGlobaisService.buscarDadosGlobaisStorage().then((dg) => {
          if (dg) {
            let loc = new Localizacao();
            loc.latitude = res.latitude;
            loc.longitude = res.longitude;
            loc.codUsuario = dg.usuario.codUsuario;
            loc.dataHoraCad = moment().format('YYYY-MM-DD HH:mm:ss');

            if (loc.codUsuario){
              this.geolocation.atualizarMinhaLocalizacao(loc).then(() => {
                this.geolocation.enviarLocalizacao(loc).subscribe(() => {}, error => {});
                
                this.chamadoService.sincronizarChamados(false, this.dadosGlobais.usuario.codTecnico).catch();
              }).catch();
            }
          }
        }).catch();
      }, err => {});
    }).catch();

    this.bGeolocation.start().then().catch();
  }

  private exibirToast(mensagem: string, tipo: string) {
    try { this.toast.dismiss() } catch(e) {};

    this.toast = this.toastCtrl.create({
      message: mensagem,
      duration: Config.TOAST.DURACAO,
      position: 'bottom',
      cssClass: 'toast-' + tipo
    });

    this.toast.present();
  }

  public sair() {
    this.dadosGlobaisService.apagarDadosGlobaisStorage().then(() => {
      this.nav.setRoot(this.tutorialPage);
    }).catch((err) => {});
  }
}
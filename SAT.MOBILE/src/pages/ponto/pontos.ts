import { Component } from '@angular/core';
import { Platform, LoadingController, AlertController, ToastController, ModalController } from 'ionic-angular';
import { PontoDataService } from '../../services/ponto-data';
import { DadosGlobaisService } from '../../services/dados-globais';
import moment from 'moment';
import { DadosGlobais } from '../../models/dados-globais';
import { PontoData } from '../../models/ponto-data';
import { Config } from '../../models/config';
import { Geolocation } from '@ionic-native/geolocation';
import { PontoUsuario } from '../../models/ponto-usuario';
import { PontoPage } from './ponto';


@Component({
  selector: 'pontos-page',
  templateUrl: 'pontos.html'
})
export class PontosPage {
  dg: DadosGlobais;
  dataAtual: any;
  dataAtualFormatada: any;
  pontosData: PontoData[] = [];
  botaoPontoStatus: boolean = false;

  constructor(
    private pontoDataService: PontoDataService,
    private dadosGlobaisService: DadosGlobaisService,
    private platform: Platform,
    private loadingCtrl: LoadingController,
    private geolocation: Geolocation,
    private alertCtrl: AlertController,
    private toastCtrl: ToastController,
    private modalCtrl: ModalController
  ) {}

  ngOnInit() {
    this.carregarDadosGlobais()
      .then(() => this.carregarDatasEPontosUsuario(null))
      .then(() => this.verificarStatusBotaoPonto())
      .catch(() => {});

    this.dataAtual = moment();
    this.dataAtualFormatada = this.dataAtual.format('DD/MM/YYYY');
  }
  
  public telaPonto(pontoData: PontoData, index: number) {
    const modal = this.modalCtrl.create(PontoPage, { pontoData: pontoData, index: index });
    modal.present();
    modal.onDidDismiss((pontoData: PontoData, index: string) => {
    });
  }

  private carregarDadosGlobais(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage().then((dados) => {
        this.dg = dados;

        if (!this.dg.usuario.codTecnico) {
          reject();
          return;
        }
        
        resolve(true);
      })
      .catch((err) => { reject(false) });
    });
  }

  private verificarStatusBotaoPonto() {
    this.pontosData.forEach(data => {
      if (data.dataRegistro ==  moment().format('YYYY-MM-DD')) {
        this.botaoPontoStatus = true;
      }      
    });
  }

  public carregarDatasEPontosUsuario(refresher, verbose: boolean=true): any {  
    if (!navigator.onLine) {
      if (verbose) this.exibirToast(Config.MSG.INTERNET_OFFLINE, Config.TOAST.ERROR);
      if (refresher) refresher.complete(); 
    }

    const loader = this.loadingCtrl.create({ content: "Carregando Pontos...", enableBackdropDismiss: true });
    if (verbose) loader.present();
    
    this.pontoDataService.buscarPontosDataStorage().then((pontosData: PontoData[]) => {
      this.pontosData = pontosData;

      this.pontoDataService.enviarPontoDataApi(pontosData, this.dg.usuario.codUsuario).subscribe((pontosData: PontoData[]) => {
        this.pontosData = pontosData;

        this.pontosData.forEach((data, i) => {
          this.pontosData[i].pontosUsuario = data.pontosUsuario.sort((a,b) => { 
            return (a.dataHoraRegistro > b.dataHoraRegistro) ? 1 : ((b.dataHoraRegistro > a.dataHoraRegistro) ? -1 : 0)
          }); 
        });

        if (verbose) loader.dismiss();
        if (refresher) refresher.complete(); 
        this.verificarStatusBotaoPonto();
        this.pontoDataService.atualizarPontosDataStorage(this.pontosData);
      }, e => {
        this.exibirToast('Não foi possível carregar os registros.', Config.TOAST.ERROR);
        if (verbose) loader.dismiss();
        if (refresher) refresher.complete();
      })
    }).catch();
  }

  public registrarPonto() {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: `Deseja registrar seu ponto em ${this.pontosData[0].dataRegistro} ${moment().format('HH:mm:ss')}?`,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.platform.ready().then(() => {
              this.geolocation.getCurrentPosition(Config.POS_CONFIG).then((location) => {
                let pontoUsuario: PontoUsuario = new PontoUsuario();
                pontoUsuario.dataHoraRegistro = `${this.pontosData[0].dataRegistro} ${moment().format('HH:mm:ss')}`;
                pontoUsuario.codUsuario = this.dg.usuario.codUsuario;
                pontoUsuario.latitude = location.coords.latitude;
                pontoUsuario.longitude = location.coords.longitude;
                pontoUsuario.indAtivo = 1;
                pontoUsuario.sincronizado = false;

                this.pontosData[0].pontosUsuario.push(pontoUsuario);
                this.pontoDataService.atualizarPontosDataStorage(this.pontosData);
              }).catch((er) => {
                this.exibirToast(`Favor ativar a localizacao.`, Config.TOAST.ERROR);
              });
            }).catch((er) => {
              this.exibirToast(`Erro ao registrar o ponto.`, Config.TOAST.ERROR);
            });
          }
        }
      ]
    });

    confirmacao.present();
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
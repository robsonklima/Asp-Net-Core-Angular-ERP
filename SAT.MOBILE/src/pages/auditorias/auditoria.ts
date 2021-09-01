import { Component, ViewChild } from '@angular/core';
import { Slides, ToastController, ModalController, AlertController, ViewController, Platform, NavParams, NavController } from 'ionic-angular';
import { Config } from '../../models/config';
import { AssinaturaPage } from '../assinatura/assinatura';
import { Auditoria } from '../../models/auditoria';
import { AcessorioVeiculo } from '../../models/acessorio-veiculo';
import { NgForm } from '@angular/forms';
import { AppAvailability } from '@ionic-native/app-availability';
import { AndroidPermissions } from '@ionic-native/android-permissions';
import { Diagnostic } from '@ionic-native/diagnostic';
import { Camera } from '@ionic-native/camera';
import { Foto } from '../../models/foto';
import moment from 'moment';
import { Market } from '@ionic-native/market';
import { DadosGlobaisService } from '../../services/dados-globais';
import { DadosGlobais } from '../../models/dados-globais';
import { AuditoriaService } from '../../services/auditoria';
import { LoadingFactory } from '../../factories/loading-factory';


@Component({
  selector: 'auditoria-page',
  templateUrl: 'auditoria.html'
})
export class AuditoriaPage {
  dg: DadosGlobais;
  @ViewChild(Slides) slides: Slides;
  tituloSlide: string;
  auditoria: Auditoria = new Auditoria();
  
  constructor(
    private platform: Platform,
    private navParams: NavParams,
    private navCtrl: NavController,
    private dadosGlobaisService: DadosGlobaisService,
    private market: Market,
    private appAvailability: AppAvailability,
    private diagnostic: Diagnostic,
    private androidPerm: AndroidPermissions,
    private camera: Camera,
    private toastCtrl: ToastController,
    private modalCtrl: ModalController,
    private alertCtrl: AlertController,
    private viewCtrl: ViewController,
    private loadingFactory: LoadingFactory,
    private auditoriaService: AuditoriaService
  ) {
    this.auditoria = this.navParams.get('auditoria');
  }

  ionViewWillEnter() {
    this.carregarDadosGlobais();
    this.configurarSlide();
  }

  public telaAssinaturaTecnico() {
    const modal = this.modalCtrl.create(AssinaturaPage, { paginaOrigem: "AUDITORIA_TECNICO" });
    modal.present();
    modal.onDidDismiss((assinatura: string) => {
      this.auditoria.assinaturaTecnico = assinatura;
    });
  }

  private carregarDadosGlobais(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage().then((dados) => {
        this.dg = dados;

        resolve(true);
      })
      .catch((err) => { reject(false) });
    });
  }

  public salvarDadosCondutor(f: NgForm) {
    this.auditoria.condutor.nome = f.value.nome;
    this.auditoria.condutor.matricula = f.value.matricula;
    this.auditoria.condutor.rg = f.value.rg;
    this.auditoria.condutor.cpf = f.value.cpf;
    this.auditoria.condutor.cnh = f.value.cnh;
    this.auditoria.condutor.cnhValidade = f.value.cnhValidade;
    this.auditoria.condutor.categorias = f.value.cnhCategorias;
    this.auditoria.condutor.finalidadesUso = f.value.finalidadesUso;
    this.auditoria.usuario.filial.nomeFilial = f.value.filial;
    this.auditoria.usuario.codTecnico = this.dg.usuario.codTecnico;

    this.exibirToast('Dados do condutor salvos com sucesso', Config.TOAST.SUCCESS);
    this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
    this.configurarSlide();
  }

  public salvarDadosVeiculo(f: NgForm) {
    this.auditoria.auditoriaVeiculo.placa = f.value.placa;
    this.auditoria.auditoriaVeiculo.odometro = f.value.odometro;
    this.auditoria.auditoriaVeiculo.codAuditoriaVeiculoTanque = f.value.codAuditoriaVeiculoTanque;

    console.log(this.auditoria);
    

    this.exibirToast('Dados do veículo salvos com sucesso', Config.TOAST.SUCCESS);
    this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
    this.configurarSlide();
  }

  public tirarFoto(modalidade: string) {
    this.platform.ready().then(() => {
      if (!this.platform.is('cordova')) {
        this.exibirToast(Config.MSG.RECURSO_NATIVO, Config.TOAST.ERROR);
        return;
      }

      this.appAvailability.check(Config.OPEN_CAMERA).then((yes: boolean) => {
        this.diagnostic.requestRuntimePermissions([ this.diagnostic.permission.WRITE_EXTERNAL_STORAGE, this.diagnostic.permission.CAMERA ]).then(() => {
          this.androidPerm.requestPermissions([ this.androidPerm.PERMISSION.WRITE_EXTERNAL_STORAGE, this.androidPerm.PERMISSION.CAMERA ]).then(() => {
            this.camera.getPicture({
              quality: Config.FOTO.QUALITY,
              targetWidth: Config.FOTO.WIDTH,
              targetHeight: Config.FOTO.HEIGHT,
              destinationType: this.camera.DestinationType.DATA_URL,
              encodingType: this.camera.EncodingType.JPEG,
              mediaType: this.camera.MediaType.PICTURE,
              saveToPhotoAlbum: false,
              allowEdit: false,
              sourceType: 1,
              correctOrientation: false
            }).then(imageData => {
              let foto = new Foto();
              foto.nome = moment().format('YYYYMMDDHHmmss') + "_" + '_' + modalidade;
              foto.str = 'data:image/jpeg;base64,' + imageData;
              foto.modalidade = modalidade;
              this.auditoria.auditoriaVeiculo.fotos.push(foto);
              this.camera.cleanup().catch();
            }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_FOTO) });
          }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_FOTO) });
        }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_PERMISSAO_CAMERA) });
      },
      (no: boolean) => {
        this.exibirToast('Favor instalar o aplicativo Open Camera', Config.TOAST.ERROR);
        setTimeout(() => { this.market.open('net.sourceforge.opencamera') }, 2500);
        return;
      }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_RESPOSTA_DISPOSITIVO) });
    }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_RESPOSTA_DISPOSITIVO) });
  }

  public removerFoto(modalidade: string) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir esta foto?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Excluir',
          handler: () => {
            if (this.auditoria.auditoriaVeiculo.fotos.length > 0) {
              this.auditoria.auditoriaVeiculo.fotos = this.auditoria.auditoriaVeiculo.fotos.filter((f) => {
                return (f.modalidade != modalidade);
              });
            }
          }
        }
      ]
    });

    confirmacao.present();
  }

  public verificarExistenciaFoto(modalidade: string): boolean {
    if (typeof(this.auditoria.auditoriaVeiculo.fotos) !== 'undefined') {
      if (this.auditoria.auditoriaVeiculo.fotos.length > 0) {
        let fotos = this.auditoria.auditoriaVeiculo.fotos.filter((foto) => { return (foto.modalidade == modalidade) });

        if (fotos.length > 0) return true;
      }
    }

    return false;
  }

  public carregarFoto(modalidade: string): string {
    if (typeof(this.auditoria.auditoriaVeiculo) !== 'undefined' && typeof(this.auditoria.auditoriaVeiculo.fotos) !== 'undefined') {
      if (this.auditoria.auditoriaVeiculo.fotos.length > 0) {
        let fotos = this.auditoria.auditoriaVeiculo.fotos.filter((foto) => { return (foto.modalidade == modalidade) });

        if (fotos.length > 0) return fotos[0].str;
      }
    }

    switch (modalidade) {
      case 'AUD_FRONTAL':
        return 'assets/imgs/aud_1.png';
      case 'AUD_FRONTAL_LAT_ESQ':
        return 'assets/imgs/aud_2.png';
      case 'AUD_ODOMETRO':
        return 'assets/imgs/aud_3.png';
      case 'AUD_TRAS_LAT_DIR':
        return 'assets/imgs/aud_4.png';
      case 'AUD_INTERNA':
        return 'assets/imgs/aud_5.png';
      case 'AUD_ITENS_SEG':
        return 'assets/imgs/aud_6.png';
      default:
        return 'assets/imgs/no-photo.png';
    }
  }

  public salvarCondicoesVeiculo() {
    this.exibirToast('Componentes e acessórios salvos com sucesso', Config.TOAST.SUCCESS);
    this.slides.slideTo(this.slides.getActiveIndex() + 1, 500);
    this.configurarSlide();
  }

  public formatarCampo(event: any, mascara: string) {
    var i = event.target.value.length;
    var saida = mascara.substring(1,0);
    var texto = mascara.substring(i)
    
    if (texto.substring(0,1) != saida) {
      event.target.value += texto.substring(0,1);
    }
  }

  private configurarSlide() {
    let i = this.slides.getActiveIndex();

    switch (i) {
      case 0:
        this.tituloSlide = `Dados do Condutor`;
        this.slides.lockSwipeToPrev(true);
        this.slides.lockSwipeToNext(false);
      break;

      case 1:
        this.tituloSlide = `Dados do Veículo`;
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(false);
      break;

      case 2:
        this.tituloSlide = `Condições do Veículo`;
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(false);
      break;

      case 3:
        this.tituloSlide = `Assinatura`;
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(true);
      break;
    }
  }

  public selecionarAcessorio(acessorio: AcessorioVeiculo, e: any) {
    if (!e) {
      const prompt = this.alertCtrl.create({
        title: 'Justificativa',
        message: `Justifique a inconformidade do item ${acessorio.nome}`,
        inputs: [
          {
            name: 'justificativa',
            placeholder: 'Justificativa'
          },
        ],
        buttons: [
          {
            text: 'Cancelar',
            handler: res => {}
          },
          {
            text: 'Salvar',
            handler: res => {
              for (let i = 0; i < this.auditoria.auditoriaVeiculo.acessorios.length; i++) {
                if (this.auditoria.auditoriaVeiculo.acessorios[i] .nome === acessorio.nome) {
                  this.auditoria.auditoriaVeiculo.acessorios[i].justificativa = res.justificativa;
                }
              }
            }
          }
        ]
      });
      prompt.present();
    }

    for (let i = 0; i < this.auditoria.auditoriaVeiculo.acessorios.length; i++) {
      if (this.auditoria.auditoriaVeiculo.acessorios[i].nome === acessorio.nome) {
        this.auditoria.auditoriaVeiculo.acessorios[i].selecionado = e;
      }
    }
  }

  public salvarAuditoria() {
    if (!this.validarCamposObrigatorios()) return;

    const confirm = this.alertCtrl.create({
      title: 'Finalizar Auditoria e Salvar?',
      message: 'Você deseja finalizar sua auditoria e enviar os dados para o servidor?',
      buttons: [
        {
          text: 'Não',
          handler: () => {}
        },
        {
          text: 'Sim',
          handler: () => {
            this.loadingFactory.exibir("Enviando dados... Por favor aguarde");

            this.auditoriaService.enviarAuditoriaApi(this.auditoria).subscribe(() => {
                this.navCtrl.popToRoot().then(() => {
                  this.loadingFactory.encerrar();
                  this.exibirToast('Auditoria enviada com sucesso');
                });
            }, err => {
              this.loadingFactory.encerrar();
              this.exibirToast('Erro ao enviar auditoria. Tente novamente', Config.TOAST.ERROR);
            })
          }
        }
      ]
    });
    confirm.present();
  }

  private validarCamposObrigatorios(): boolean {
    if (typeof(this.auditoria.condutor) === 'undefined') {
      this.exibirToast('Insira os dados do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.condutor.nome) {
      this.exibirToast('Insira o nome do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.condutor.matricula) {
      this.exibirToast('Insira a matrícula do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.condutor.rg) {
      this.exibirToast('Insira o RG do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.condutor.cpf) {
      this.exibirToast('Insira o CPF do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.condutor.categorias) {
      this.exibirToast('Insira as categorias da CNH do condutor', Config.TOAST.ERROR);
      return false;
    }
    
    if (!this.auditoria.condutor.categorias.length) {
      this.exibirToast('Insira as categorias da CNH do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.condutor.cnh) {
      this.exibirToast('Insira a CNH do condutor', Config.TOAST.ERROR);
      return false;
    }

    if (typeof(this.auditoria.auditoriaVeiculo) === 'undefined') {
      this.exibirToast('Insira os dados do veículo', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.auditoriaVeiculo.placa) {
      this.exibirToast('Insira a placa do veículo', Config.TOAST.ERROR);
      return false;
    }
      
    if (typeof(this.auditoria.auditoriaVeiculo.fotos) === 'undefined') {
      this.exibirToast('Insira as fotos do veículo', Config.TOAST.ERROR);
      return false;
    }

    if (!this.auditoria.assinaturaTecnico) {
      this.exibirToast('Insira a sua assinatura', Config.TOAST.ERROR);
      return false;
    }

    if (this.auditoria.auditoriaVeiculo.fotos.length < 6) {
      this.exibirToast('Favor inserir as fotos', Config.TOAST.ERROR);

      return;
    }
    
    if (this.auditoria.auditoriaVeiculo.acessorios.length == 0) {
      this.exibirToast('Favor inserir os acessórios', Config.TOAST.ERROR);
      
      return;
    }

    return true;
  }

  public sair() {
    const confirm = this.alertCtrl.create({
      title: 'Deseja sair?',
      message: 'Você perderá todas as informações inseridas',
      buttons: [
        {
          text: 'Não',
          handler: () => {}
        },
        {
          text: 'Sim',
          handler: () => {
            this.viewCtrl.dismiss();
          }
        }
      ]
    });
    confirm.present();
  }

  private exibirAlerta(msg: string) {
    const alerta = this.alertCtrl.create({
      title: 'Alerta!',
      subTitle: msg,
      buttons: ['OK']
    });

    alerta.present();
  }

  private exibirToast(mensagem: string, tipo: string='info', posicao: string=null) {
    const toast = this.toastCtrl.create({
      message: mensagem, 
      duration: 3000, 
      position: posicao || 'bottom', 
      cssClass: 'toast-' + tipo
    });
    
    toast.present();
  }
}
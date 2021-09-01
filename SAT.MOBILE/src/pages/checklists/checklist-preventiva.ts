import { Component, ViewChild } from '@angular/core';
import { NavParams, AlertController, ViewController, ToastController, Slides, Platform } from 'ionic-angular';
import { Chamado } from '../../models/chamado';
import { Config } from '../../models/config';
import { NgForm } from '@angular/forms';
import { ChamadoService } from '../../services/chamado';
import { ChecklistPreventivaItem } from '../../models/checklist-preventiva-item';
import { Foto } from '../../models/foto';
import { Camera } from '@ionic-native/camera';
import { Diagnostic } from '@ionic-native/diagnostic';
import { AndroidPermissions } from '@ionic-native/android-permissions';
import moment from 'moment';
import { DadosGlobaisService } from '../../services/dados-globais';
import { DadosGlobais } from '../../models/dados-globais';


@Component({
  selector: 'checklist-preventiva-page',
  templateUrl: 'checklist-preventiva.html'
})
export class ChecklistPreventivaPage {
  @ViewChild(Slides) slides: Slides;
  tituloSlide: string;
  chamado: Chamado;
  itensNaoChecados: number;
  dg: DadosGlobais;

  constructor(
    private navParams: NavParams,
    private camera: Camera,
    private diagnostic: Diagnostic,
    private androidPerm: AndroidPermissions,
    private platform: Platform,
    private alertCtrl: AlertController,
    private viewCtrl: ViewController,
    private toastCtrl: ToastController,
    private chamadoService: ChamadoService,
    private dadosGlobaisService: DadosGlobaisService
  ) {
    this.chamado = this.navParams.get('chamado');
  }

  ionViewWillEnter() {
    this.carregarDadosGlobais();
    this.configurarSlide();
    this.itensNaoChecados = this.chamado.checklistPreventiva.itens.filter((i) => { return (i.checado === 0 && i.obs === null) }).length;
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

  public fecharModalConfirmacao() {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Ao sair você perderá todas as informações inseridas ou modificadas?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.viewCtrl.dismiss(this.chamado);
          }
        }
      ]
    });

    confirmacao.present();
  }

  private validarCamposObrigatorios(): boolean {
    if (this.platform.is('cordova') && !this.chamado.checklistPreventiva.fotos.length) {
      this.exibirToast('Você esqueceu de incluir as fotos do checklist', Config.TOAST.ERROR);

      return false;
    }

    return true;
  }

  public salvarInformacoesAmbiente(form: NgForm) {
    this.chamado.checklistPreventiva.tensaoSemCarga = Number(form.value.tensaoSemCarga);
    this.chamado.checklistPreventiva.tensaoComCarga = Number(form.value.tensaoComCarga);
    this.chamado.checklistPreventiva.tensaoEntreTerraENeutro = Number(form.value.tensaoEntreTerraENeutro);
    this.chamado.checklistPreventiva.temperatura = Number(form.value.temperatura);
    this.chamado.checklistPreventiva.redeEstabilizada = Number(form.value.redeEstabilizada);
    this.chamado.checklistPreventiva.possuiNoBreak = Number(form.value.possuiNoBreak);
    this.chamado.checklistPreventiva.possuiArCondicionado = Number(form.value.possuiArCondicionado);
    this.chamado.checklistPreventiva.justificativa = form.value.justificativa;

    console.log(this.chamado);
    this.chamadoService.atualizarChamado(this.chamado).then(() => {
      this.configurarSlide();
      this.slides.slideTo(1, 500);
    });
  }

  public tirarFoto() {
    this.chamadoService.buscarStatusExecucao().then(executando => {
      if (executando) {
        this.exibirToast(Config.MSG.AGUARDE_ALGUNS_INSTANTES, Config.TOAST.WARNING);
        return;
      }

      this.platform.ready().then(() => {
        if (!this.platform.is('cordova')) {
          this.exibirToast(Config.MSG.RECURSO_NATIVO, Config.TOAST.WARNING);
          return;
        }
  
        this.diagnostic.requestRuntimePermissions([
          this.diagnostic.permission.WRITE_EXTERNAL_STORAGE,
          this.diagnostic.permission.CAMERA
        ]).then(() => {
          this.androidPerm.requestPermissions([
            this.androidPerm.PERMISSION.WRITE_EXTERNAL_STORAGE,
            this.androidPerm.PERMISSION.CAMERA
          ]).then(() => {
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
              let foto: Foto = new Foto();
              foto.codOS = this.chamado.codOs;
              foto.nome = moment().format('YYYYMMDDHHmmss') + '_' + this.chamado.codOs + '_CHECKLIST_PREVENTIVA';
              foto.str = 'data:image/jpeg;base64,' + imageData;
              foto.modalidade = "CHECKLIST_PREVENTIVA";
              this.chamado.checklistPreventiva.fotos.push(foto);
              this.chamadoService.atualizarChamado(this.chamado);
              this.camera.cleanup().catch();
            }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_AO_ACESSAR_CAMERA) });
          }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_AO_ACESSAR_CAMERA) });
        }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_AO_ACESSAR_CAMERA) });
      }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_AO_ACESSAR_CAMERA) });
    });    
  }

  public removerFoto(i: number) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: `Deseja excluir a foto ${i+1}?`,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.chamado.checklistPreventiva.fotos.splice(i, 1);
            this.chamadoService.atualizarChamado(this.chamado);
          }
        }
      ]
    });

    confirmacao.present();
  }

  public selecionarItem(item: ChecklistPreventivaItem, e: any) {
    if (!e || item.obrigatorioObs) {
      const prompt = this.alertCtrl.create({
        title: 'Observação',
        message: `${ item.descricao }`,
        enableBackdropDismiss: false,
        inputs: [
          {
            name: 'obs',
            placeholder: 'Observação'
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
              if (!res.obs || res.obs.trim() == '') {
                this.exibirToast('Favor preencher a observação', Config.TOAST.ERROR);
                
                return false
              }

              // Atualiza Obs
              for (let i = 0; i < this.chamado.checklistPreventiva.itens.length; i++) {
                if (this.chamado.checklistPreventiva.itens[i].descricao === item.descricao) {
                  this.chamado.checklistPreventiva.itens[i].obs = res.obs;
                }
              }
            }
          }
        ]
      });

      prompt.present();
    }

    // Atualiza checado
    for (let i = 0; i < this.chamado.checklistPreventiva.itens.length; i++) {
      if (this.chamado.checklistPreventiva.itens[i].descricao === item.descricao && e) {
        this.chamado.checklistPreventiva.itens[i].checado = 1;
      }
    }

    this.itensNaoChecados = this.chamado.checklistPreventiva.itens.filter((i) => { return (i.checado === 0 && i.obs === null) }).length;
  }

  public salvarChecklist() {
    if (!this.validarCamposObrigatorios()) return;

    const confirm = this.alertCtrl.create({
      title: 'Finalizar Checklist?',
      message: 'Você deseja salvar e finalizar este checklist?',
      buttons: [
        {
          text: 'Não',
          handler: () => {}
        },
        {
          text: 'Sim',
          handler: () => {
            this.chamado.checklistPreventiva.realizado = true;
            this.chamado.checklistPreventiva.codUsuarioCad = this.dg.usuario.codUsuario;

            this.chamadoService.atualizarChamado(this.chamado).then(() => {
              this.viewCtrl.dismiss(this.chamado);
            });
          }
        }
      ]
    });
    confirm.present();
  }

  private configurarSlide() {
    let i = this.slides.getActiveIndex();

    switch (i) {
      case 0:
        this.tituloSlide = `Imagens`;
        this.slides.lockSwipeToPrev(true);
        this.slides.lockSwipeToNext(false);
      break;

      case 1:
        this.tituloSlide = `Ambiente`;
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(false);
      break;

      case 2:
        this.tituloSlide = `Checklist`;
        this.slides.lockSwipeToPrev(false);
        this.slides.lockSwipeToNext(true);
      break;
    }
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
      duration: Config.TOAST.DURACAO, 
      position: posicao || 'bottom', 
      cssClass: 'toast-' + tipo
    });
    
    toast.present();
  }
}
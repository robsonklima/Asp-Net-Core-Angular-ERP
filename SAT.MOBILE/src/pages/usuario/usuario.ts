import { Component } from '@angular/core';
import { DadosGlobaisService } from '../../services/dados-globais';
import { DadosGlobais } from '../../models/dados-globais';
import { AlertController, LoadingController, Platform, ToastController } from 'ionic-angular';
import { NgForm } from '@angular/forms';
import { GeolocationService } from '../../services/geo-location';
import { Cep } from '../../models/cep';
import { Usuario } from '../../models/usuario';
import { UsuarioService } from '../../services/usuario';
import { AppAvailability } from '@ionic-native/app-availability';
import { Diagnostic } from '@ionic-native/diagnostic';
import { AndroidPermissions } from '@ionic-native/android-permissions';
import { Camera } from '@ionic-native/camera';
import { Config } from '../../models/config';
import { Market } from '@ionic-native/market';


@Component({
  selector: 'usuario-page',
  templateUrl: 'usuario.html'
})
export class UsuarioPage {
  dg: DadosGlobais;

  constructor(
    private alertCtrl: AlertController,
    private platform: Platform,
    private diagnostic: Diagnostic,
    private androidPerm: AndroidPermissions,
    private camera: Camera,
    private market: Market,
    private toastCtrl: ToastController,
    private appAvailability: AppAvailability,
    private loadingCtrl: LoadingController,
    private dadosGlobaisService: DadosGlobaisService,
    private geolocationService: GeolocationService,
    private usuarioService: UsuarioService
  ) {

  }

  ionViewWillEnter() {
    this.carregarDadosGlobais().catch(() => { });
  }

  private carregarDadosGlobais(): Promise<DadosGlobais> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage()
        .then((dados) => {
          if (dados) {
            this.dg = dados;
          }

          resolve(dados);
        })
        .catch((err) => {
          reject(new Error(err.message))
        });
    });
  }

  public salvarUsuario(form: NgForm) {
    this.dg.usuario.nome = form.value.nomeUsuario;
    this.dg.usuario.fonePerto = form.value.telefonePerto;
    this.dg.usuario.fone = form.value.telefonePessoal;
    this.dg.usuario.cpf = form.value.cpf;
    this.dg.usuario.cep = form.value.cep.replace(/\D/g, '');
    this.dg.usuario.endereco = form.value.endereco;
    this.dg.usuario.numero = form.value.numero;
    this.dg.usuario.bairro = form.value.bairro;
    this.dg.usuario.siglaUF = form.value.estado;
    this.dg.usuario.cidade = form.value.cidade;
    this.dg.usuario.enderecoComplemento = form.value.complemento;

    const loading = this.loadingCtrl.create({
      content: 'Aguarde...'
    });
    loading.present();

    this.usuarioService.atualizarUsuarioApi(this.dg.usuario).subscribe((res: Usuario) => {
      loading.dismiss().then(() => {
        this.dadosGlobaisService.insereDadosGlobaisStorage(this.dg);
        this.exibirToast('Usuário salvo com sucesso!');
      });
    }, e => {
      loading.dismiss().then(() => {
        this.exibirToast('Não foi possível salvar o usuário!');
      });
    })
  }

  public formatarCampo(event: any, mascara: string) {
    var i = event.target.value.length;
    var saida = mascara.substring(1, 0);
    var texto = mascara.substring(i)
    if (texto.substring(0, 1) != saida) {
      event.target.value += texto.substring(0, 1);
    }

    if (mascara === '#####-###' && event.target.value.length == 9) {
      this.geolocationService.buscarDetalhesPorCepApi(event.target.value).subscribe((res: Cep) => {
        this.dg.usuario.cep = res.cep;
        this.dg.usuario.endereco = res.logradouro;
        this.dg.usuario.bairro = res.bairro;
        this.dg.usuario.siglaUF = res.uf;
        this.dg.usuario.cidade = res.localidade;
      }, e => { });
    }
  }

  public tirarFoto() {
    const confirm = this.alertCtrl.create({
      title: 'Instruções',
      message: `Por favor, utilize a câmera do aparelho para tirar uma foto 3x4. 
                Certifique-se de estar utilizando o crachá e uniforme da empresa.`,
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Ok',
          handler: () => {
            this.platform.ready().then(() => {
              this.diagnostic.requestRuntimePermissions([this.diagnostic.permission.WRITE_EXTERNAL_STORAGE, this.diagnostic.permission.CAMERA]).then(() => {
                this.androidPerm.requestPermissions([this.androidPerm.PERMISSION.WRITE_EXTERNAL_STORAGE, this.androidPerm.PERMISSION.CAMERA]).then(() => {
                  this.camera.getPicture({
                    quality: 80,
                    targetWidth: 320,
                    destinationType: this.camera.DestinationType.DATA_URL,
                    encodingType: this.camera.EncodingType.JPEG,
                    mediaType: this.camera.MediaType.PICTURE,
                    saveToPhotoAlbum: false,
                    sourceType: 1,
                  }).then(imageData => {

                    const loading = this.loadingCtrl.create({
                      content: 'Aguarde...'
                    });
                    loading.present();

                    this.dg.usuario.foto = 'data:image/jpeg;base64,' + imageData;

                    this.usuarioService.atualizarUsuarioApi(this.dg.usuario).subscribe((res: Usuario) => {
                      loading.dismiss().then(() => {
                        this.dadosGlobaisService.insereDadosGlobaisStorage(this.dg);
                        this.exibirToast('Foto salva com sucesso! A foto pode levar até 5 minutos para atualizar!');
                      });
                    }, e => {
                      loading.dismiss().then(() => {
                        this.exibirToast('Não foi possível salvar a foto! Tente novamente!');
                      });
                    });

                    this.camera.cleanup().catch();
                  }).catch();
                }).catch();
              }).catch();
            },
              (no: boolean) => {
                this.exibirToast('Favor instalar o aplicativo Open Camera', Config.TOAST.ERROR);
                setTimeout(() => { this.market.open('net.sourceforge.opencamera') }, 2500);
                return;
              }).catch(() => { this.exibirAlerta(Config.MSG.ERRO_RESPOSTA_DISPOSITIVO) });
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

  private exibirToast(mensagem: string, tipo: string = 'info', posicao: string = null) {
    const toast = this.toastCtrl.create({
      message: mensagem,
      duration: Config.TOAST.DURACAO,
      position: posicao || 'bottom',
      cssClass: 'toast-' + tipo
    });

    toast.present();
  }
}
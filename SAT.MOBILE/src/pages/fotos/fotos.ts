import { Component } from '@angular/core';
import { ViewController, NavParams, ToastController, AlertController } from 'ionic-angular';

import { Camera } from "@ionic-native/camera";

import { Chamado } from '../../models/chamado';
import { Foto } from '../../models/foto';

import { ChamadoService } from '../../services/chamado';
import { Config } from '../../models/config';

@Component({
  selector: 'fotos-page',
  templateUrl: 'fotos.html'
})
export class FotosPage {
  chamado: Chamado;
  modalidade: string;
  foto: Foto;
  qtdMaximaFotos: number = Config.QTD_MAX_FOTOS_POR_ATENDIMENTO;

  constructor(
      private viewCtrl: ViewController,
      private navParams: NavParams,
      private toastCtrl: ToastController,
      private alertCtrl: AlertController,
      private camera: Camera,
      private chamadoService: ChamadoService
  ) { 
    this.chamado = this.navParams.get('chamado');
    this.modalidade = this.navParams.get('modalidade');
  }
  
  public tirarFoto() {
    this.camera.getPicture({
      quality: Config.FOTO.QUALITY,
      destinationType: this.camera.DestinationType.DATA_URL,
      encodingType: this.camera.EncodingType.JPEG,
      mediaType: this.camera.MediaType.PICTURE,
      correctOrientation: true,
      targetWidth: Config.FOTO.WIDTH,
      targetHeight: Config.FOTO.HEIGHT,
      allowEdit: true,
    }).then(imageData => {
      this.foto = new Foto();
      this.foto.nome = this.chamado.codOs.toString() + '_' + this.chamado.rats[0].numRat + '_' + new Date().getUTCMilliseconds().toString();
      this.foto.str = 'data:image/jpeg;base64,' + imageData;
      this.foto.modalidade = this.modalidade;
      this.chamado.rats[0].fotos.push(this.foto);
      this.chamadoService.atualizarChamado(this.chamado);
      this.camera.cleanup();

      setTimeout(() => { this.exibirToast("Foto adicionada com sucesso") }, 500);
    }).catch(err => {
      setTimeout(() => { this.exibirToast(err) }, 500);
    });
  }

  public removerFoto(i: number) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir esta foto?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => {}
        },
        {
          text: 'Confirmar',
          handler: () => {
            this.chamado.rats[0].fotos.splice(i, 1);

            this.exibirToast('Foto removida com sucesso')
              .then(() => {
                this.chamadoService.atualizarChamado(this.chamado);
              })
              .catch(() => {});
          }
        }
      ]
    });

    confirmacao.present();
  }

  private exibirToast(mensagem: string): Promise<any> {
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: mensagem, duration: 3000, position: 'bottom'
      });

      resolve(toast.present());
    });
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}
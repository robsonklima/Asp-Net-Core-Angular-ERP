import { Component, ViewChild } from '@angular/core';
import { NavParams, ViewController, ModalController, ToastController } from 'ionic-angular';
import { SignaturePad } from 'angular2-signaturepad/signature-pad';
import { ChamadoConfPage } from '../chamados/chamado-conf';
import { Laudo } from '../../models/laudo';
import { Chamado } from '../../models/chamado';
import { NgForm } from '@angular/forms';
import { Config } from '../../models/config';


@Component({
  selector: 'assinatura-page',
  templateUrl: 'assinatura.html',
})
export class AssinaturaPage {
  paginaOrigem: string;
  tituloPagina: string;
  laudo: Laudo;
  chamado: Chamado;
  @ViewChild(SignaturePad) public signaturePad: SignaturePad;
  public signaturePadOptions: Object = { 'minWidth': 2, 'canvasWidth': 320, 'canvasHeight': 200 };
  public signatureImage: string;

  constructor(
    private viewCtrl: ViewController,
    private navParams: NavParams,
    private modalCtrl: ModalController,
    private toastCtrl: ToastController
  ) {
    this.paginaOrigem = this.navParams.get('paginaOrigem');
    this.laudo = this.navParams.get('laudo');
    this.chamado = this.navParams.get('chamado');
  }

  ionViewWillEnter() {
    if (this.paginaOrigem == "LAUDO_TECNICO" || this.paginaOrigem == "AUDITORIA_TECNICO") {
      this.tituloPagina = "Assinatura do Técnico";
    }

    if (this.paginaOrigem == "LAUDO_CLIENTE" || this.paginaOrigem == "RAT_CLIENTE") {
      this.tituloPagina = "Assinatura do Cliente";
    }

    if (this.paginaOrigem == "RAT_TECNICO") {
      this.tituloPagina = "Assinatura do Técnico";
    }
  }

  public salvarAssinatura(form: NgForm) {
    this.signatureImage = this.signaturePad.toDataURL();

    if (this.paginaOrigem == "LAUDO_TECNICO") {
      this.laudo.assinaturaTecnico = this.signatureImage;
      this.viewCtrl.dismiss(this.laudo);
    }

    if (this.paginaOrigem == "LAUDO_CLIENTE") {
      this.laudo.assinaturaCliente = this.signatureImage;
      this.viewCtrl.dismiss(this.laudo);
    }

    if (this.paginaOrigem == "RAT_CLIENTE") {
      if (this.chamado.rats.length > 0) {
        this.chamado.rats[0].emailCliente = form.value.emailCliente;
      }
  
      if (form.value.emailCliente && !/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(form.value.emailCliente))
      {
        this.exibirToast(Config.MSG.ERRO_EMAIL_INVALIDO, Config.TOAST.ERROR);
        return;
      }

      this.chamado.rats[0].assinaturaCliente = this.signatureImage;
      this.viewCtrl.dismiss(this.chamado);
    }

    if (this.paginaOrigem == "RAT_TECNICO") {
      this.chamado.rats[0].assinaturaTecnico = this.signatureImage;
      this.viewCtrl.dismiss(this.chamado);
    }

    if (this.paginaOrigem == "AUDITORIA_TECNICO") {
      this.viewCtrl.dismiss(this.signatureImage);
    }
  }

  public limparAssinatura() {
    this.signaturePad.clear();
  }

  public telaChamadoConf() {
    const modal = this.modalCtrl.create(ChamadoConfPage, { chamado: this.chamado });
    modal.present();
    modal.onDidDismiss(() => {
      
    });
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

  public fecharModal() {
    if (this.paginaOrigem == 'LAUDO_CLIENTE' || this.paginaOrigem == 'LAUDO_TECNICO')
     this.viewCtrl.dismiss(this.laudo);
    else if (this.paginaOrigem == 'RAT_CLIENTE')
      this.viewCtrl.dismiss(this.chamado);
  }
}
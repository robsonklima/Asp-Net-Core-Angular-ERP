import { Component } from '@angular/core';
import { ViewController, NavController, LoadingController, ToastController } from 'ionic-angular';

import { AjudaTopico } from '../../models/ajuda-topico';

import { AjudaDetalhePage } from './ajuda-detalhe';

import { AjudaTopicoService } from '../../services/ajuda-topico';

@Component({
  selector: 'ajuda-lista-page',
  templateUrl: 'ajuda-lista.html'
})
export class AjudaListaPage {
  topicos: AjudaTopico[] = [];

  constructor(
    private viewCtrl: ViewController,
    private navCtrl: NavController,
    private toastCtrl: ToastController,
    private loadingCtrl: LoadingController,
    private atService: AjudaTopicoService
  ) {}

  ngOnInit() {
    this.carregarTopicos();
  }

  private carregarTopicos() {
    const loading = this.loadingCtrl.create({ content: 'Aguarde...' });
    loading.present();

    this.atService.buscarTopicosApi().subscribe((topicos) => {
      this.topicos = topicos;

      loading.dismiss()
    },
    err => {
      this.exibirToast(`Não foi possível carregar os tópicos`).then(() => {
        loading.dismiss().then(() => {
          this.fecharModal();
        });
      });
    });
  }

  public telaAjudaDetalhe(topico: AjudaTopico) {
    this.navCtrl.push(AjudaDetalhePage, { topico: topico });
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
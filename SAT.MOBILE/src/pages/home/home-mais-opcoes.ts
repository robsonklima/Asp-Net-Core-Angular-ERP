import { Component } from '@angular/core';
import { ViewController, ModalController } from 'ionic-angular';

import { AjudaListaPage } from '../ajuda/ajuda-lista';
import { ProblemaPage } from '../problema/problema';
import { SobrePage } from '../sobre/sobre';
import { LogsPage } from '../logs/logs';

@Component({
  selector: 'home-mais-opcoes-page',
  templateUrl: 'home-mais-opcoes.html'
})
export class HomeMaisOpcoesPage {
  constructor(
    private viewCtrl: ViewController,
    private modalCtrl: ModalController
  ) {}

  public telaAjuda() {
    const modal = this.modalCtrl.create(AjudaListaPage);

    this.viewCtrl.dismiss().then(() => {
      modal.present();
    }).catch();

    modal.onDidDismiss(() => {});
  }

  public telaProblema() {
    const modal = this.modalCtrl.create(ProblemaPage);

    this.viewCtrl.dismiss().then(() => {
      modal.present();
    }).catch();

    modal.onDidDismiss(() => {});
  }

  public telaSobre() {
    const modal = this.modalCtrl.create(SobrePage);

    this.viewCtrl.dismiss().then(() => {
      modal.present();
    }).catch();

    modal.onDidDismiss(() => {});
  }

  public telaLogs() {
    const modal = this.modalCtrl.create(LogsPage);

    this.viewCtrl.dismiss().then(() => {
      modal.present();
    }).catch();

    modal.onDidDismiss(() => {});
  }
}
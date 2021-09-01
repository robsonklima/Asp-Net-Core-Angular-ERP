import { Component } from '@angular/core';
import { ViewController } from 'ionic-angular';
import moment from 'moment';
import { Config } from '../../models/config';

@Component({
  selector: 'sobre-page',
  templateUrl: 'sobre.html'
})
export class SobrePage {
  versaoApp: string;
  anoAtual: string;

  constructor(
    private viewCtrl: ViewController
  ) { }

  ionViewWillEnter() {
    this.versaoApp = Config.VERSAO_APP;
    this.anoAtual = moment().format('YYYY');
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}
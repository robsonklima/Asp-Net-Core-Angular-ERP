import { Component } from '@angular/core';
import { NavParams, ViewController } from 'ionic-angular';

import { Peca } from "../../models/peca";

@Component({
  selector: 'peca-page',
  templateUrl: 'peca.html'
})
export class PecaPage {
  peca: Peca;

  constructor(
    private viewCtrl: ViewController,
    private navParams: NavParams,
  ) {
    this.peca = this.navParams.get('peca');
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}
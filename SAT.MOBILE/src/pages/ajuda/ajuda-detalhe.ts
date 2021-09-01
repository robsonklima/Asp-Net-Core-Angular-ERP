import { Component } from '@angular/core';
import { NavParams} from 'ionic-angular';

import { AjudaTopico } from '../../models/ajuda-topico';

@Component({
  selector: 'ajuda-detalhe-page',
  templateUrl: 'ajuda-detalhe.html'
})
export class AjudaDetalhePage {
  topico: AjudaTopico;

  constructor(
    private navParams: NavParams
  ) {
    this.topico = this.navParams.get('topico');
  }
}
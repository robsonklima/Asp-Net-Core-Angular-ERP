import { Component} from '@angular/core';
import { ViewController, NavParams } from 'ionic-angular';
import { Chamado } from '../../models/chamado';


@Component({
  selector: 'chamado-conf-page',
  templateUrl: 'chamado-conf.html',
})
export class ChamadoConfPage {
  chamado: Chamado;

  constructor(
    private viewCtrl: ViewController,
    private navParams: NavParams
  ) {
    this.chamado = this.navParams.get('chamado');
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}
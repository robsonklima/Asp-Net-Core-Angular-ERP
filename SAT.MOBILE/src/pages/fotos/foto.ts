import { Component} from '@angular/core';
import { ViewController, NavParams } from 'ionic-angular';


@Component({
  selector: 'foto-page',
  templateUrl: 'foto.html',
})
export class FotoPage {
  foto: string;

  constructor(
    private viewCtrl: ViewController,
    private navParams: NavParams
  ) {
    this.foto = this.navParams.get('foto');
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}
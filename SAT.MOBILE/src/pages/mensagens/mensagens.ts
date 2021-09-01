import { Component } from '@angular/core';
import { NavParams, NavController } from 'ionic-angular';
import { DadosGlobais } from '../../models/dados-globais';
import { MensagemTecnico } from '../../models/mensagem-tecnico';
import { MensagemPage } from './mensagem';


@Component({
  selector: 'mensagens-page',
  templateUrl: 'mensagens.html'
})
export class MensagensPage {
  dg: DadosGlobais;
  mensagensTecnico: MensagemTecnico[] = [];

  constructor(
    private navCtrl: NavController,
    private navParams: NavParams,
  ) {
    this.mensagensTecnico = this.navParams.get('mensagens');
  }

  public telaMensagemTecnico(mensagemTecnico: MensagemTecnico) {
    this.navCtrl.push(MensagemPage, { mensagemTecnico: mensagemTecnico });
  }
}
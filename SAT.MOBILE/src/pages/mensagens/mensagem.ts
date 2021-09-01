import { Component } from '@angular/core';
import { NavParams } from 'ionic-angular';
import { MensagemTecnico } from '../../models/mensagem-tecnico';
import { DadosGlobais } from '../../models/dados-globais';

import { MensagemTecnicoService } from '../../services/mensagem-tecnico';
import { DadosGlobaisService } from '../../services/dados-globais';


@Component({
  selector: 'mensagem-page',
  templateUrl: 'mensagem.html'
})
export class MensagemPage {
  dg: DadosGlobais;
  mensagemTecnico: MensagemTecnico;

  constructor(
    private navParams: NavParams,
    private mtService: MensagemTecnicoService,
    private dadosGlobaisService: DadosGlobaisService
  ) {
    this.mensagemTecnico = this.navParams.get('mensagemTecnico');
  }

  ionViewWillEnter() {
    this.carregarDadosGlobais().then(() => {
      if (this.mensagemTecnico.indLeitura == 1) return;

      this.mtService.enviarMensagemTecnicoApi(this.mensagemTecnico).subscribe((r) => {
        this.mensagemTecnico.indLeitura = 1;
      }, () => {});
    }).catch(() => {});
  }

  private carregarDadosGlobais(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage().then((dados) => {
        this.dg = dados;

        resolve(true);
      })
      .catch((err) => { reject(false) });
    });
  }
}
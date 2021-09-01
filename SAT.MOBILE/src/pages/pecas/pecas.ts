import { Component } from '@angular/core';
import { LoadingController, ModalController } from 'ionic-angular';

import { PecaPage } from '../peca/peca';

import { Peca } from "../../models/peca";
import { DadosGlobais } from '../../models/dados-globais';

import { PecaService } from "../../services/peca";
import { DadosGlobaisService } from '../../services/dados-globais';

@Component({
  selector: 'pecas-page',
  templateUrl: 'pecas.html'
})
export class PecasPage {
  dadosGlobais: DadosGlobais;
  pecas: Peca[] = [];
  spinner: boolean;

  constructor(
    private loadingCtrl: LoadingController,
    private modalCtrl: ModalController,
    private pecaService: PecaService,
    private dadosGlobaisService: DadosGlobaisService
  ) {}

  ionViewWillEnter() {
    this.carregarDadosGlobais();
  }

  public telaPeca(peca: Peca) {
    const modal = this.modalCtrl.create(PecaPage, { peca: peca });
    modal.present();
    modal.onDidDismiss(() => {});
  }

  private carregarPecasStorage(): Promise<any> {
    return new Promise((resolve, reject) => {
      resolve(
        this.pecaService.buscarPecasStorage()
          .then(
          (pecas: Peca[]) => {
            this.pecas = pecas.sort( function(a,b) { return (a.codMagnus > b.codMagnus) ? 1 : 
              ((b.codMagnus > a.codMagnus) ? -1 : 0) }); 
          })
          .catch(err => { })
      );
    });
  }

  public filtrarPecas(ev: any) {
    let val = ev.target.value;

    if (val && val.trim() != '') {
      if (val.length < 4) {
        return
      }
    }

    this.spinner = true;

    this.carregarPecasStorage().then(() => {
      this.spinner = false;

      this.pecas = this.pecas.filter((peca) => {
        if (val && val.trim() != '') {
          return (peca.nomePeca.toString().toLowerCase().indexOf(val.toLowerCase()) > -1
            || peca.codMagnus.toString().toLowerCase().indexOf(val.toLowerCase()) > -1);
        }
      })
    });
  }

  public atualizarBDLocal() {
    const loading = this.loadingCtrl.create({ content: 'Atualizando a listagem de peças junto ao servidor' });
    loading.present();

     this.pecaService.buscarPecasApi()
      .subscribe(() => {
        loading.dismiss();
      }, () => { loading.dismiss() });
  }

  private carregarDadosGlobais() {
    this.dadosGlobaisService.buscarDadosGlobaisStorage()
      .then((dados: DadosGlobais) => {
        if (dados) 
          this.dadosGlobais = dados;
      })
      .catch((err) => {});
  }
}
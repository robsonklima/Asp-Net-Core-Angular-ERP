import { Component } from '@angular/core';
import { AlertController, NavParams, ToastController, ViewController } from 'ionic-angular';

import { Chamado } from "../../models/chamado";
import { Peca } from "../../models/peca";
import { PecaService } from "../../services/peca";
import { ChamadoService } from "../../services/chamado";

@Component({
  selector: 'rat-detalhe-peca-page',
  templateUrl: 'rat-detalhe-peca.html'
})
export class RatDetalhePecaPage {
  chamado: Chamado;
  ratDetalheIndex: number;
  inputPesquisar: string;
  pecas: Peca[] = [];
  spinner: boolean;
  qtd = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 ];

  constructor(
    private viewCtrl: ViewController,
    private alertCtrl: AlertController,
    private toastCtrl: ToastController,
    private navParams: NavParams,
    private pecaService: PecaService,
    private chamadoService: ChamadoService
  ) {
    this.chamado = this.navParams.get('chamado');
    this.ratDetalheIndex = this.navParams.get('i');
  }

  public adicionarPeca(peca: Peca) {
    if (!this.verificaPecaJaAdicionada(peca)) {
      this.chamado.rats[0].ratDetalhes[this.ratDetalheIndex].pecas.push({
        codPeca: peca.codPeca,
        codMagnus: peca.codMagnus,
        nomePeca: peca.nomePeca,
        qtd: 1
      });
      this.limparPesquisa();
      this.chamadoService.atualizarChamado(this.chamado);
    } else {
      this.exibirToast("Esta peça já foi adicionada");
    }
  }

  public alterarQtdPeca(qtd: number, i: number) {
    this.chamado.rats[0].ratDetalhes[this.ratDetalheIndex].pecas[i].qtd = qtd;
    this.chamadoService.atualizarChamado(this.chamado);
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

  public removerPeca(peca: Peca) {
    const confirmacao = this.alertCtrl.create({
      title: 'Confirmação',
      message: 'Deseja excluir esta peça?',
      buttons: [
        {
          text: 'Cancelar',
          handler: () => { }
        },
        {
          text: 'Confirmar',
          handler: () => {
            let i = this.chamado.rats[0].ratDetalhes[this.ratDetalheIndex].pecas
              .map(function (p) { return p.codPeca; }).indexOf(peca.codPeca);

            if (i > -1) {
              this.chamado.rats[0].ratDetalhes[this.ratDetalheIndex].pecas.splice(i, 1);
              this.exibirToast("Peça removida com sucesso")
              this.chamadoService.atualizarChamado(this.chamado);
            }
          }
        }
      ]
    });

    confirmacao.present();
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

  private limparPesquisa() {
    this.pecas.splice(0, this.pecas.length);
    this.inputPesquisar = "";
  }

  private verificaPecaJaAdicionada(peca: Peca) {
    let res: boolean;

    this.chamado.rats[0].ratDetalhes[this.ratDetalheIndex].pecas.forEach((p, i) => {
      if (p.codMagnus.toString().toLowerCase().indexOf(peca.codMagnus.toString().toLowerCase()) > -1)
        res = true;
    });

    return res;
  }

  private exibirToast(mensagem: string): Promise<any> {
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: mensagem, duration: 2500, position: 'bottom'
      });

      resolve(toast.present());
    });
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }
}
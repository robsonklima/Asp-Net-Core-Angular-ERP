import { Component } from '@angular/core';
import { ViewController, LoadingController, ToastController, ModalController } from 'ionic-angular';

import { ChamadoFechadoPage } from './chamado-fechado';

import { DadosGlobais } from '../../models/dados-globais';
import { Chamado } from '../../models/chamado';

import { DadosGlobaisService } from '../../services/dados-globais';
import { ChamadoService } from '../../services/chamado';

import moment from 'moment';
import { Config } from '../../models/config';

@Component({
  selector: 'chamados-fechados-page',
  templateUrl: 'chamados-fechados.html'
})
export class ChamadosFechadosPage {
  dg: DadosGlobais;
  qtdChamadosAExibir: Number = 20;
  chamados: Chamado[] = [];

  constructor(
    private toastCtrl: ToastController,
    private viewCtrl: ViewController,
    private loadingCtrl: LoadingController,
    private dgService: DadosGlobaisService,
    private chamadosService: ChamadoService,
    private modalCtrl: ModalController
  ) {}

  ionViewDidLoad() {
    this.carregarDadosGlobais().then(() => {
      this.carregarChamadosFechadosApi();
    }).catch(() => {});
  }

  private carregarChamadosFechadosApi() {
    const loading = this.loadingCtrl.create({ 
      content: 'Obtendo chamados do servidor...' 
    });
    loading.present();

    this.chamadosService.buscarChamadosFechadosApi(this.dg.usuario.codTecnico)
      .subscribe((chamados: Chamado[]) => {
        this.chamados = chamados.sort(function(a, b) { 
          return (moment(a.dataHoraFechamento, 'YYYY-MM-DD HH:mm').isBefore(moment(b.dataHoraFechamento, 'YYYY-MM-DD HH:mm')) ? -1 : (moment(a.dataHoraFechamento, 'YYYY-MM-DD HH:mm').isAfter(moment(b.dataHoraFechamento, 'YYYY-MM-DD HH:mm')) ? 1 : 0));
        });

        loading.dismiss()
      },
      err => {
        this.exibirToast('Não foi possível carregar seus chamados');
        loading.dismiss()
      });
  }

  private carregarDadosGlobais(): Promise<DadosGlobais> {
    return new Promise((resolve, reject) => {
      this.dgService.buscarDadosGlobaisStorage()
        .then((dados) => {
          if (dados)
            this.dg = dados;
            resolve(dados);
        })
        .catch((err) => {
          reject(new Error(err.message))
        });
    });
  }

  public telaChamadoFechado(chamado: Chamado) {
    const modal = this.modalCtrl.create(
      ChamadoFechadoPage, { chamado: chamado });
    modal.present();
    modal.onDidDismiss(() => {});
  }

  public fecharModal() {
    this.viewCtrl.dismiss();
  }

  private exibirToast(mensagem: string): Promise<any> {
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: mensagem, duration: Config.TOAST.DURACAO, position: 'bottom'
      });

      resolve(toast.present());
    });
  }
}
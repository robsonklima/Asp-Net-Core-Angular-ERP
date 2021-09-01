import { Chamado } from '../../models/chamado';
import { Component } from '@angular/core';
import { AlertController, LoadingController, 
  NavParams, ViewController, ModalController } from 'ionic-angular';

import { HistoricoDetalhePage } from '../historico/historico-detalhe';
import { EquipamentoContratoService } from '../../services/equipamento-contrato';

@Component({
  selector: 'historico-lista-page',
  templateUrl: 'historico-lista.html'
})
export class HistoricoListaPage {
  chamado: Chamado;
  chamados: Chamado[] = [];
  qtdChamadosAExibir: Number = 20;

  constructor(
    private viewCtrl: ViewController,
    private navParams: NavParams,
    private loadingCtrl: LoadingController,
    private alertCtrl: AlertController,
    private modalCtrl: ModalController,
    private ecService: EquipamentoContratoService
  ) {
    this.chamado = this.navParams.get('chamado');
    this.carregarEquipamentoContratoHistOs();
  }

  public telaEquipHistDetalhe(chamado: Chamado) {
    const modal = this.modalCtrl.create(
      HistoricoDetalhePage, { chamado: chamado });
    modal.present();
    modal.onDidDismiss(() => {});
  }

  private carregarEquipamentoContratoHistOs() {
    if (!this.chamado.equipamentoContrato.codEquipContrato) 
      return;  
    
    const loading = this.loadingCtrl.create({ 
      content: 'Aguarde...' 
    });
    loading.present();

    this.ecService.buscarEquipamentoContratoHistOs(
      this.chamado.equipamentoContrato.codEquipContrato
      ).subscribe((chamados: Chamado[]) => {
        if (chamados) 
          this.chamados = chamados.sort(function(a, b) { 
            return ((a.codOs > b.codOs) ? -1 : 
              ((a.codOs < b.codOs) ? 1 : 0));  
          });
        loading.dismiss()
      },
      err => {
        this.exibirAlerta(`Não foi possível buscar o 
          histórico do equipamento`);
        loading.dismiss()
      });
  }

  fecharModal() {
    this.viewCtrl.dismiss();
  }

  exibirAlerta(msg: string) {
    const alerta = this.alertCtrl.create({
      title: 'Alerta!',
      subTitle: msg,
      buttons: ['OK']
    });

    alerta.present();
  }
}
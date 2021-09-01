import { Component, ViewChild } from '@angular/core';
import { ToastController } from 'ionic-angular';

import { Config } from '../../models/config';
import { Chart } from 'chart.js';

import { DadosGlobaisService } from '../../services/dados-globais';
import { IndicadorService } from '../../services/indicador';
import { DadosGlobais } from '../../models/dados-globais';

@Component({
  selector: 'indicador-tecnico-page',
  templateUrl: 'indicador-tecnico.html'
})
export class IndicadorTecnicoPage {
  dg: DadosGlobais;
  slaMelhorTecnico: String; 

  grfSLATecnicoLabels: string[] = [];
  grfSLATecnicoValues: number[] = [];
  grfSLATecnicoColors: string[] = [];
  @ViewChild('grfSLATecnico') grfSLATecnico;

  grfPendenciaTecnicoLabels: string[] = [];
  grfPendenciaTecnicoValues: number[] = [];
  grfPendenciaTecnicoColors: string[] = [];
  @ViewChild('grfPendenciaTecnico') grfPendenciaTecnico;

  grfReincidenciaTecnicoLabels: string[] = [];
  grfReincidenciaTecnicoValues: number[] = [];
  grfReincidenciaTecnicoColors: string[] = [];
  @ViewChild('grfReincidenciaTecnico') grfReincidenciaTecnico;

  constructor(
    private toastCtrl: ToastController,
    private dadosGlobaisService: DadosGlobaisService,
    private indicadorService: IndicadorService
  ) {}

  ionViewDidLoad() {
    this.carregarDadosGlobais()
      .then(() => {
        this.carregarSLATecnicoApi()
          .then(() => this.carregarSLATecnicoGrafico()).then(() => {
            this.carregarSLAMelhorTecnicoApi().catch(() => {});
          }).catch(() => {});
          
        this.carregarPendenciaTecnicoApi()
          .then(() => this.carregarPendenciaTecnicoGrafico()).catch(() => {});

        this.carregarReincidenciaTecnicoApi()
          .then(() => this.carregarReincidenciaTecnicoGrafico()).catch(() => {});
      })
      .catch(() => {});
  }

  private carregarDadosGlobais(): Promise<DadosGlobais> {
    return new Promise((resolve, reject) => {
      this.dadosGlobaisService.buscarDadosGlobaisStorage()
        .then((dados) => {
          if (dados)
            this.dg = dados;
            resolve(this.dg);
        })
        .catch((err) => {
          reject(new Error(err.message))
        });
    });
  }

  private carregarSLATecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfSLATecnicoApi(this.dg.usuario.codTecnico)
      .subscribe(dados => {
        dados.forEach((d, i) => {
          this.grfSLATecnicoLabels.push('Fora do Prazo (%)');
          this.grfSLATecnicoValues.push(Number(d.percForaPrazo));
          this.grfSLATecnicoColors.push(Config.COR_RGB.VERMELHO);

          this.grfSLATecnicoLabels.push('No Prazo (%)');
          this.grfSLATecnicoValues.push(Number(d.percNoPrazo));
          this.grfSLATecnicoColors.push(Config.COR_RGB.VERDE);
        });
        
        resolve();
      },
      err => { reject(); });
    });
  }

  private carregarSLATecnicoGrafico() {
    this.grfSLATecnico = new Chart(this.grfSLATecnico.nativeElement, {
      type: 'doughnut',
      data: {
        labels: this.grfSLATecnicoLabels,
        datasets: [{
          label: 'Percentual',
          data: this.grfSLATecnicoValues,
          backgroundColor: this.grfSLATecnicoColors
        }]
      }
    });
  }

  private carregarSLAMelhorTecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfSLAMelhorTecnicoApi()
      .subscribe(dados => {
        if (dados) {
          this.slaMelhorTecnico = dados[0].percNoPrazo;
        }
          
        resolve();
      },
      err => { reject(); });
    });
  }

  private carregarPendenciaTecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfPendenciaTecnicoApi(this.dg.usuario.codTecnico)
      .subscribe(dados => {
        dados.forEach((d, i) => {
          this.grfPendenciaTecnicoLabels.push('Pendente (%)');
          this.grfPendenciaTecnicoValues.push(Number(d.percentualChamadosPendentes.replace(',', '.')));
          this.grfPendenciaTecnicoColors.push(Config.COR_RGB.VERMELHO);

          this.grfPendenciaTecnicoLabels.push('Não Pendente (%)');
          this.grfPendenciaTecnicoValues.push(Number(d.percentualChamadosNaoPendentes.replace(',', '.')));
          this.grfPendenciaTecnicoColors.push(Config.COR_RGB.VERDE);
        });

        resolve();
      },
      err => { reject(); });
    });
  }

  private carregarPendenciaTecnicoGrafico() {
    this.grfPendenciaTecnico = new Chart(this.grfPendenciaTecnico.nativeElement, {
      type: 'doughnut',
      data: {
        labels: this.grfPendenciaTecnicoLabels,
        datasets: [{
          label: 'Percentual',
          data: this.grfPendenciaTecnicoValues,
          backgroundColor: this.grfPendenciaTecnicoColors
        }]
      }
    });
  }

  private carregarReincidenciaTecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfReincidenciaTecnicoApi(this.dg.usuario.codTecnico)
      .subscribe(dados => {
        dados.forEach((d, i) => {
          this.grfReincidenciaTecnicoLabels.push('Reincidente (%)');
          this.grfReincidenciaTecnicoValues.push(Number(d.percChamadosReincidentes.replace(',', '.')));
          this.grfReincidenciaTecnicoColors.push(Config.COR_RGB.VERMELHO);

          this.grfReincidenciaTecnicoLabels.push('Não Reincidente (%)');
          this.grfReincidenciaTecnicoValues.push(Number(d.percChamadosNaoReincidentes.replace(',', '.')));
          this.grfReincidenciaTecnicoColors.push(Config.COR_RGB.VERDE);
        });

        resolve();
      },
      err => { reject(); });
    });
  }

  private carregarReincidenciaTecnicoGrafico() {
    this.grfReincidenciaTecnico = new Chart(this.grfReincidenciaTecnico.nativeElement, {
      type: 'doughnut',
      data: {
        labels: this.grfReincidenciaTecnicoLabels,
        datasets: [{
          label: 'Percentual',
          data: this.grfReincidenciaTecnicoValues,
          backgroundColor: this.grfReincidenciaTecnicoColors
        }]
      }
    });
  }

  public exibirToast(message: string): Promise<any> {    
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: message, duration: 2000, position: 'bottom'
      });

      resolve(toast.present());
    });
  }
}
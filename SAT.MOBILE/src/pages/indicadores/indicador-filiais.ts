import { Component, ViewChild } from '@angular/core';

import { Chart } from 'chart.js';
import { Config } from '../../models/config';

import { DadosGlobais } from '../../models/dados-globais';
import { DadosGlobaisService } from '../../services/dados-globais';
import { IndicadorService } from '../../services/indicador';

@Component({
  selector: 'indicador-filiais-page',
  templateUrl: 'indicador-filiais.html'
})
export class IndicadorFiliaisPage {
  dg: DadosGlobais;
  
  grfSLAFiliaisLabels: string[] = [];
  grfSLAFiliaisValues: number[] = [];
  grfSLAFiliaisColors: string[] = [];
  @ViewChild('grfSLAFiliais') grfSLAFiliais;

  grfPendenciaFiliaisLabels: string[] = [];
  grfPendenciaFiliaisValues: number[] = [];
  grfPendenciaFiliaisColors: string[] = [];
  @ViewChild('grfPendenciaFiliais') grfPendenciaFiliais;

  grfReincidenciaFiliaisLabels: string[] = [];
  grfReincidenciaFiliaisValues: number[] = [];
  grfReincidenciaFiliaisColors: string[] = [];
  @ViewChild('grfReincidenciaFiliais') grfReincidenciaFiliais;

  grfDispBBFilialLabels: string[] = [];
  grfDispBBFilialValues: number[] = [];
  grfDispBBFilialColors: string[] = [];
  @ViewChild('grfDispBBFilial') grfDispBBFilial;
  
  constructor(
    private indicadorService: IndicadorService,
    private dgService: DadosGlobaisService
  ) {}

  ionViewDidLoad() {
    this.carregarDadosGlobais()
      .then(() => {
        this.carregarSLAFiliaisApi()
          .then(() => this.carregarSLAFiliaisGrafico()).catch(() => {});
        
        this.carregarPendenciaFiliaisApi()
          .then(() => this.carregarPendenciaFiliaisGrafico()).catch(() => {});

        this.carregarReincidenciaFiliaisApi()
          .then(() => this.carregarReincidenciaFiliaisGrafico()).catch(() => {});
          
        this.carregarDispBBFilialApi()
          .then(() => this.carregarDispBBFilialGrafico()).catch(() => {});
      })
      .catch(() => {});
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

  private carregarSLAFiliaisApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfSLAFilialApi().subscribe(dados => {
        var percentualMax = Math.max.apply(Math, dados.map(function(d){ return d.percentual; }))
        
        this.grfSLAFiliaisLabels.push('MELHOR');
        this.grfSLAFiliaisValues.push(percentualMax);
        this.grfSLAFiliaisColors.push(this.carregarCorSLAFiliais(percentualMax));

        let sum = 0;
        dados.forEach((d, i) => {
          if (d.nomeFilial.indexOf(this.dg.usuario.filial.nomeFilial) == 0) {
            this.grfSLAFiliaisLabels.push('MINHA');
            this.grfSLAFiliaisValues.push(d.percentual);
            this.grfSLAFiliaisColors.push(this.carregarCorSLAFiliais(d.percentual));
          }

          sum += Number(d.percentual);
        });

        let avg = sum / dados.length;
        this.grfSLAFiliaisLabels.push('MÉDIA');
        this.grfSLAFiliaisValues.push(Number(avg.toFixed(2)));
        this.grfSLAFiliaisColors.push(this.carregarCorSLAFiliais(avg));
        
        resolve(dados);
      },
      err => { reject(); });
    });
  }

  private carregarCorSLAFiliais(percentual: Number) {
    if (percentual > 95) {
      return Config.COR_RGB.VERDE;
    } else if (percentual > 90.01 && percentual <= 95) {
      return Config.COR_RGB.LARANJA;
    } else {
      return Config.COR_RGB.VERMELHO;
    }
  }

  private carregarSLAFiliaisGrafico() {
    var horizontalBarChartData = {
			labels: this.grfSLAFiliaisLabels,
			datasets: [{
				label: '%',
				backgroundColor: this.grfSLAFiliaisColors,
				borderColor: this.grfSLAFiliaisColors,
				borderWidth: 1,
				data: this.grfSLAFiliaisValues
			}]
		};

    this.grfSLAFiliais = new Chart(this.grfSLAFiliais.nativeElement, {
      type: 'bar',
      data: horizontalBarChartData,
      options: {
        elements: {
          rectangle: {
            borderWidth: 2,
          }
        },
        responsive: true, 
        maintainAspectRatio: false,
        legend: false,
        title: {
          display: false,
          text: 'SLA por Filiais'
        },
        scales: { 
          xAxes: [{ ticks: { beginAtZero: false } }]
        }
      }
    });
  }

  private carregarPendenciaFiliaisApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfPendenciaFilialApi().subscribe(dados => {
        var percentualMax = Math.min.apply(Math, dados.map(function(d){ return d.percentual; }))
          
        this.grfPendenciaFiliaisLabels.push('MELHOR');
        this.grfPendenciaFiliaisValues.push(percentualMax);
        this.grfPendenciaFiliaisColors.push(this.carregarCorPendenciaFiliais(percentualMax));

        let sum = 0;
        dados.forEach((d, i) => {
          if (d.nomeFilial.indexOf(this.dg.usuario.filial.nomeFilial) == 0) {
            this.grfPendenciaFiliaisLabels.push('MINHA');
            this.grfPendenciaFiliaisValues.push(d.percentual);
            this.grfPendenciaFiliaisColors.push(this.carregarCorPendenciaFiliais(d.percentual));
          }

          sum += Number(d.percentual);
        });

        let avg = sum / dados.length;
        this.grfPendenciaFiliaisLabels.push('MÉDIA');
        this.grfPendenciaFiliaisValues.push(Number(avg.toFixed(2)));
        this.grfPendenciaFiliaisColors.push(this.carregarCorPendenciaFiliais(avg));
        
        resolve(dados);
      },
      err => { reject(); });
    });
  }

  private carregarCorPendenciaFiliais(percentual: Number) {
    if (percentual <= 3) {
      return Config.COR_RGB.VERDE;
    } else if (percentual > 3 && percentual < 5) {
      return Config.COR_RGB.LARANJA;
    } else {
      return Config.COR_RGB.VERMELHO;
    }
  }

  private carregarPendenciaFiliaisGrafico() {
    var horizontalBarChartData = {
			labels: this.grfPendenciaFiliaisLabels,
			datasets: [{
				label: '%',
				backgroundColor: this.grfPendenciaFiliaisColors,
				borderColor: this.grfPendenciaFiliaisColors,
				borderWidth: 1,
				data: this.grfPendenciaFiliaisValues
			}]
		};

    this.grfPendenciaFiliais = new Chart(this.grfPendenciaFiliais.nativeElement, {
      type: 'bar',
      data: horizontalBarChartData,
      options: {
        elements: {
          rectangle: {
            borderWidth: 2,
          }
        },
        responsive: true, 
        maintainAspectRatio: false,
        legend: false,
        title: {
          display: false,
          text: 'Pendência por Filiais'
        },
        scales: { 
          xAxes: [{ ticks: { beginAtZero: false } }]
        }
      }
    });
  }

  private carregarReincidenciaFiliaisApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfReincidenciaFilialApi().subscribe(dados => {
        var percentualMax = Math.min.apply(Math, dados.map(function(d){ return d.percentual; }))
          
        this.grfReincidenciaFiliaisLabels.push('MELHOR');
        this.grfReincidenciaFiliaisValues.push(percentualMax);
        this.grfReincidenciaFiliaisColors.push(this.carregarCorReincidenciaFiliais(percentualMax));

        let sum = 0;
        dados.forEach((d, i) => {
          if (d.nomeFilial.indexOf(this.dg.usuario.filial.nomeFilial) == 0) {
            this.grfReincidenciaFiliaisLabels.push('MINHA');
            this.grfReincidenciaFiliaisValues.push(d.percentual);
            this.grfReincidenciaFiliaisColors.push(this.carregarCorReincidenciaFiliais(d.percentual));
          }

          sum += Number(d.percentual);
        });

        let avg = sum / dados.length;
        this.grfReincidenciaFiliaisLabels.push('MÉDIA');
        this.grfReincidenciaFiliaisValues.push(Number(avg.toFixed(2)));
        this.grfReincidenciaFiliaisColors.push(this.carregarCorReincidenciaFiliais(avg));
        
        resolve(dados);
      },
      err => { reject(); });
    });
  }

  private carregarCorReincidenciaFiliais(percentual: Number) {
    if (percentual < 35) {
      return Config.COR_RGB.VERDE;
    } else {
      return Config.COR_RGB.VERMELHO;
    }
  }

  private carregarReincidenciaFiliaisGrafico() {
    var horizontalBarChartData = {
			labels: this.grfReincidenciaFiliaisLabels,
			datasets: [{
				label: '%',
				backgroundColor: this.grfReincidenciaFiliaisColors,
				borderColor: this.grfReincidenciaFiliaisColors,
				borderWidth: 1,
				data: this.grfReincidenciaFiliaisValues
			}]
		};

    this.grfReincidenciaFiliais = new Chart(this.grfReincidenciaFiliais.nativeElement, {
      type: 'bar',
      data: horizontalBarChartData,
      options: {
        elements: {
          rectangle: {
            borderWidth: 2,
          }
        },
        responsive: true, 
        maintainAspectRatio: false,
        legend: false,
        title: {
          display: false,
          text: 'Reincidência por Filiais'
        },
        scales: { 
          xAxes: [{ ticks: { beginAtZero: false } }]
        }
      }
    });
  }

  private carregarDispBBFilialApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfDispBBFilialApi(this.dg.usuario.filial.codFilial)
        .subscribe(dados => {
          dados.forEach((d, i) => {
            this.grfDispBBFilialLabels.push(d[0].replace('DISP.', 'C') + '-' + d[2].substring(0, 4));
            this.grfDispBBFilialValues.push(Number(d[1]));

            if (Number(d[1]) > Number(d[3])) {
              this.grfDispBBFilialColors.push(Config.COR_RGB.VERDE);
            } else {
              this.grfDispBBFilialColors.push(Config.COR_RGB.VERMELHO);
            }
          });
          
          resolve(dados);
        },
        err => { reject(); });
    });
  }

  private carregarDispBBFilialGrafico() {
    this.grfDispBBFilial = new Chart(this.grfDispBBFilial.nativeElement, {
      type: 'bar',
      data: {
        labels: this.grfDispBBFilialLabels,
        datasets: [{
          label: '%',
          data: this.grfDispBBFilialValues,
          backgroundColor: this.grfDispBBFilialColors,
          borderWidth: 1
        }]
      },
      options: { legend: false, scales: { yAxes: [{ ticks: { beginAtZero: false } }] } }
    });
  }
}
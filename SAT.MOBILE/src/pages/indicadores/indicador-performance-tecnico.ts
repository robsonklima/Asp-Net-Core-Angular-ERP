import { Component, ViewChild } from '@angular/core';
import { NavController, ToastController, LoadingController } from 'ionic-angular';
import { Chart } from 'chart.js';

import { DadosGlobais } from '../../models/dados-globais';
import { IndicadorService } from '../../services/indicador';
import { DadosGlobaisService } from '../../services/dados-globais';
import { Config } from '../../models/config';

@Component({
  selector: 'indicador-performance-tecnico-page',
  templateUrl: 'indicador-performance-tecnico.html'
})
export class IndicadorPerformanceTecnicoPage {
  dg: DadosGlobais;
  desvioMediaAtendimentosDia: string = "";
  mediaAtendimentosDia: string = "";
  qtdOSCorretiva: string = "";
  qtdOSGeral: string = "";
  qtdOSOutrasIntervencoes: string = "";
  qtdOSPreventiva: string = "";
  desvioMediaAtendimentosDiaMelhorTecnico: string = "";
  mediaAtendimentosDiaMelhorTecnico: string = "";
  qtdOSCorretivaMelhorTecnico: string = "";
  qtdOSGeralMelhorTecnico: string = "";
  qtdOSOutrasIntervencoesMelhorTecnico: string = "";
  qtdOSPreventivaMelhorTecnico: string = "";
  qtdPecasTrocadas: string = "";
  percChamadosFechadosPecasTrocadas: string = "";
  qtdPecasPendentes: string = "";
  percChamadosFechadosPecasPendentes: string = "";
  pecasMaisTrocadas: any[] = [];
  pecasMaisPendenciadas: any[] = [];

  grfAtendimentosLabels: string[] = [];
  grfAtendimentosTecnicoValues: number[] = [];
  grfAtendimentosTecnicoColors: string[] = [];
  grfAtendimentosMelhorTecnicoValues: number[] = [];
  grfAtendimentosMelhorTecnicoColors: string[] = [];
  @ViewChild('grfAtendimentos') grfAtendimentos;

  grfMediaTecnicoLabels: string[] = [];
  grfMediaTecnicoValues: number[] = [];
  grfMediaTecnicoColors: string[] = [];
  grfMediaTecnicoMelhorTecnicoValues: number[] = [];
  grfMediaTecnicoMelhorTecnicoColors: string[] = [];
  @ViewChild('grfMediaTecnico') grfMediaTecnico;

  constructor(
    private toastCtrl: ToastController,
    private loadingCtrl: LoadingController,
    private navCtrl: NavController,
    private indicadorService: IndicadorService,
    private dadosGlobaisService: DadosGlobaisService
  ) {}

  ionViewWillEnter() {
    this.carregarDadosGlobais()
      .then(() => this.carregarGrfAcumuladoTecnicoApi().then(() => {
        this.carregarAtendimentosGrafico()
        this.carregarMediaTecnicoGrafico()
      }))
      .then(() => this.carregarGrfPecasMaisTrocadasTecnicoApi())
      .then(() => this.carregarGrfPecasMaisPendenciadasTecnicoApi())
      .catch(() => {});
  }

  private carregarGrfAcumuladoTecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      const loader = this.loadingCtrl.create({
        content: 'Carregando...'
      });
      loader.present();

      this.indicadorService.buscarGrfAcumuladoTecnicoApi(this.dg.usuario.codTecnico)
        .subscribe(dados => {
          if (dados.length > 0) {
            this.qtdOSGeral = dados[0].qtdOSGeral;
            this.qtdOSCorretiva = dados[0].qtdOSCorretiva;
            this.qtdOSPreventiva = dados[0].qtdOSPreventiva;
            this.qtdOSOutrasIntervencoes = dados[0].qtdOSOutrasIntervencoes;
            this.mediaAtendimentosDia = dados[0].mediaAtendimentosDia;
            this.desvioMediaAtendimentosDia = dados[0].desvioMediaAtendimentosDia;
            
            this.qtdOSGeralMelhorTecnico = dados[0].qtdOSGeralMelhorTecnico;
            this.qtdOSCorretivaMelhorTecnico = dados[0].qtdOSCorretivaMelhorTecnico;
            this.qtdOSPreventivaMelhorTecnico = dados[0].qtdOSPreventivaMelhorTecnico;
            this.qtdOSOutrasIntervencoesMelhorTecnico = dados[0].qtdOSOutrasIntervencoesMelhorTecnico;
            this.desvioMediaAtendimentosDiaMelhorTecnico = dados[0].desvioMediaAtendimentosDiaMelhorTecnico;
            this.mediaAtendimentosDiaMelhorTecnico = dados[0].mediaAtendimentosDiaMelhorTecnico;

            this.qtdPecasTrocadas = dados[0].qtdPecasTrocadas;
            this.percChamadosFechadosPecasTrocadas = dados[0].percChamadosFechadosPecasTrocadas;
            this.qtdPecasPendentes = dados[0].qtdOSComPecasPendentes;
            this.percChamadosFechadosPecasPendentes = dados[0].percChamadosFechadosPecasPendentes;
          }

        loader.dismiss();
        resolve(dados);
      },
      err => {
        this.exibirToast("Não foi possível carregar os dados acumulados").then(() => {
          this.navCtrl.pop();
          reject(new Error(err.message));
        }).catch(() => {})
      });
    });
  }

  private carregarAtendimentosGrafico() {
    this.grfAtendimentosLabels.push("Todos");
    this.grfAtendimentosTecnicoColors.push(Config.COR_RGB.AZUL)
    this.grfAtendimentosTecnicoValues.push(Number(this.qtdOSGeral));
    this.grfAtendimentosMelhorTecnicoColors.push(Config.COR_RGB.LARANJA)
    this.grfAtendimentosMelhorTecnicoValues.push(Number(this.qtdOSGeralMelhorTecnico));

    this.grfAtendimentosLabels.push('Corretivas');
    this.grfAtendimentosTecnicoColors.push(Config.COR_RGB.AZUL)
    this.grfAtendimentosTecnicoValues.push(Number(this.qtdOSCorretiva.replace(',', '.')));
    this.grfAtendimentosMelhorTecnicoColors.push(Config.COR_RGB.LARANJA)
    this.grfAtendimentosMelhorTecnicoValues.push(Number(this.qtdOSCorretivaMelhorTecnico.replace(',', '.')));

    this.grfAtendimentosLabels.push('Preventivas');
    this.grfAtendimentosTecnicoColors.push(Config.COR_RGB.AZUL)
    this.grfAtendimentosTecnicoValues.push(Number(this.qtdOSPreventiva.replace(',', '.')));
    this.grfAtendimentosMelhorTecnicoColors.push(Config.COR_RGB.LARANJA)
    this.grfAtendimentosMelhorTecnicoValues.push(Number(this.qtdOSPreventivaMelhorTecnico.replace(',', '.')));

    this.grfAtendimentosLabels.push('Outras');
    this.grfAtendimentosTecnicoColors.push(Config.COR_RGB.AZUL)
    this.grfAtendimentosTecnicoValues.push(Number(this.qtdOSOutrasIntervencoes.replace(',', '.')));
    this.grfAtendimentosMelhorTecnicoColors.push(Config.COR_RGB.LARANJA)
    this.grfAtendimentosMelhorTecnicoValues.push(Number(this.qtdOSOutrasIntervencoesMelhorTecnico.replace(',', '.')));

    var horizontalBarChartData = {
			labels: this.grfAtendimentosLabels,
			datasets: [
        {
          label: 'Meus Dados',
          backgroundColor: this.grfAtendimentosTecnicoColors,
				  borderColor: this.grfAtendimentosTecnicoColors,
          borderWidth: 1,
          data: this.grfAtendimentosTecnicoValues
        },
        {
          label: 'Melhor Técnico',
          backgroundColor: this.grfAtendimentosMelhorTecnicoColors,
				  borderColor: this.grfAtendimentosMelhorTecnicoColors,
          borderWidth: 1,
          data: this.grfAtendimentosMelhorTecnicoValues
        }
      ]
		};

    this.grfAtendimentos = new Chart(this.grfAtendimentos.nativeElement, {
      type: 'horizontalBar',
      data: horizontalBarChartData,
      options: {
        elements: {
          rectangle: {
            borderWidth: 2,
          }
        },
        //responsive: true, 
        maintainAspectRatio: false,
        legend: false,
        title: {
          display: false,
          text: 'Atendimentos'
        },
        scales: { 
          xAxes: [{ 
            ticks: { 
              beginAtZero: true,
              stepSize : 20
            },
            gridLines: {
              //lineWidth: 0,
              //color: Config.COR_RGB.CINZA
            },
            scaleLabel: {
              display: true,
              labelString: 'Quantidade'
            }
          }],
          yAxes: [{
            stacked: true,
            ticks: {
              min: 0,
              stepSize: 1,
              callback: function(label, index, labels) {
                return label;
              }
            },
            scaleLabel: {
              //display: true,
              //labelString: '1k = 1000'
            }
          }]
        }
      }
    });
  }

  private carregarMediaTecnicoGrafico() {
    this.grfMediaTecnicoLabels.push('Média');
    this.grfMediaTecnicoColors.push(Config.COR_RGB.AZUL)
    this.grfMediaTecnicoValues.push(Number(this.mediaAtendimentosDia.replace(',', '.')));
    this.grfMediaTecnicoMelhorTecnicoColors.push(Config.COR_RGB.LARANJA)
    this.grfMediaTecnicoMelhorTecnicoValues.push(Number(this.mediaAtendimentosDiaMelhorTecnico.replace(',', '.')));

    var horizontalBarChartData = {
			labels: this.grfMediaTecnicoLabels,
			datasets: [
        {
          label: 'Meus Dados',
          backgroundColor: this.grfMediaTecnicoColors,
				  borderColor: this.grfMediaTecnicoColors,
          borderWidth: 1,
          data: this.grfMediaTecnicoValues
        },
        {
          label: 'Melhor Técnico',
          backgroundColor: this.grfMediaTecnicoMelhorTecnicoColors,
				  borderColor: this.grfMediaTecnicoMelhorTecnicoColors,
          borderWidth: 1,
          data: this.grfMediaTecnicoMelhorTecnicoValues
        }
      ]
		};

    this.grfMediaTecnico = new Chart(this.grfMediaTecnico.nativeElement, {
      type: 'horizontalBar',
      data: horizontalBarChartData,
      options: {
        elements: {
          rectangle: {
            borderWidth: 2,
          }
        },
        responsive: true, 
        maintainAspectRatio: true,
        legend: false,
        title: {
          display: false,
          text: 'Atendimentos'
        },
        scales: { 
          xAxes: [{ 
            ticks: { 
              beginAtZero: true,
              stepSize : 1
            },
            gridLines: {
              //lineWidth: 0,
              //color: Config.COR_RGB.CINZA
            },
            scaleLabel: {
              display: true,
              labelString: 'Meta 4'
            }
          }],
          yAxes: [{
            stacked: true,
            ticks: {
              min: 0,
              stepSize: 1,
              callback: function(label, index, labels) {
                return label;
              }
            },
            scaleLabel: {
              //display: true,
              //labelString: '1k = 1000'
            }
          }]
        }
      }
    });
  }

  private carregarGrfPecasMaisTrocadasTecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfPecasMaisTrocadasTecnicoApi(this.dg.usuario.codTecnico)
        .subscribe(dados => {
          if (dados) {
            dados.forEach((d, i) => {
              this.pecasMaisTrocadas.push(d);
            });
          }

          resolve(dados);
        },
        err => {
          this.exibirToast("Não foi possível carregar as peças mais trocadas").then(() => {
            reject(new Error(err.message));
          }).catch(() => {})
        });      
    });
  }

  private carregarGrfPecasMaisPendenciadasTecnicoApi(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.indicadorService.buscarGrfPecasMaisPendenciadasTecnicoApi(this.dg.usuario.codTecnico)
        .subscribe(dados => {
          if (dados) {
            dados.forEach((d, i) => {
              this.pecasMaisPendenciadas.push(d);
            });
          }

          resolve(dados);
        },
        err => {
          this.exibirToast("Não foi possível carregar as peças mais pendenciadas").then(() => {
            reject(new Error(err.message));
          }).catch(() => {})
        });      
    });
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

  public exibirToast(message: string): Promise<any> {    
    return new Promise((resolve, reject) => {
      const toast = this.toastCtrl.create({
        message: message, duration: 2000, position: 'bottom'
      });

      resolve(toast.present());
    });
  }
}
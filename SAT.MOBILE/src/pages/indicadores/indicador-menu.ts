import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';

import { IndicadorFiliaisPage } from '../indicadores/indicador-filiais';
import { IndicadorTecnicoPage } from '../indicadores/indicador-tecnico';
import { IndicadorPerformanceTecnicoPage } from './indicador-performance-tecnico';

@Component({
  selector: 'indicador-menu-page',
  templateUrl: 'indicador-menu.html'
})
export class IndicadorMenuPage {
  constructor(
    private navCtrl: NavController,
  ) {}

  public telaIndicadorFiliais() {
    this.navCtrl.push(IndicadorFiliaisPage);
  }

  public telaIndicadorTecnico() {
    this.navCtrl.push(IndicadorTecnicoPage);
  }

  public telaIndicadorAcumuladoTecnico() {
    this.navCtrl.push(IndicadorPerformanceTecnicoPage);
  }
}
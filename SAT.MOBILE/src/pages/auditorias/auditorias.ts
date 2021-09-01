import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';

import { AuditoriaPage } from './auditoria';
import { Auditoria } from '../../models/auditoria';

import { DadosGlobais } from '../../models/dados-globais';


@Component({
  selector: 'auditorias-page',
  templateUrl: 'auditorias.html'
})
export class AuditoriasPage {
  dg: DadosGlobais;
  auditorias: Auditoria[] = [];

  constructor(
    private navCtrl: NavController,
    private navParams: NavParams
  ) {
    this.auditorias = this.navParams.get('auditorias');
  }

  public telaAuditoria(auditoria: Auditoria) {
    this.navCtrl.push(AuditoriaPage, { auditoria: auditoria });
  }
}
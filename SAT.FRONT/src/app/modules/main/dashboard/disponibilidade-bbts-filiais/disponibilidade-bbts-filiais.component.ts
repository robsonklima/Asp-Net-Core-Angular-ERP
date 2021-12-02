import { Component, Input, OnInit } from '@angular/core';
import { Filtro } from 'app/core/types/filtro.types';

@Component({
  selector: 'app-disponibilidade-bbts-filiais',
  templateUrl: './disponibilidade-bbts-filiais.component.html',
  styleUrls: ['./disponibilidade-bbts-filiais.component.css'
  ]
})
export class DisponibilidadeBbtsFiliaisComponent implements OnInit {
  @Input() filtro: Filtro;
  public disponibilidadeFilial: DisponibilidadeFilialModel[] = [];
  public loading: boolean = true;

  constructor() { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {
    for (let i = 0; i < 10; i++) {
      let d: DisponibilidadeFilialModel = new DisponibilidadeFilialModel();
      d.filial = 'FRS';
      d.indice11 = '100%';
      d.saldo11 = '85.40';
      d.indice12 = '100%';
      d.saldo12 = '85.40';
      d.indice13 = '100%';
      d.saldo13 = '85.40';
      d.indice14 = '100%';
      d.saldo14 = '85.40';
      d.indice15 = '100%';
      d.saldo15 = '85.40';
      this.disponibilidadeFilial.push(d);
    }

    this.loading = false;
  }

}

export class DisponibilidadeFilialModel {
  public filial: string;
  public indice11: string;
  public saldo11: string;
  public indice12: string;
  public saldo12: string;
  public indice13: string;
  public saldo13: string;
  public indice14: string;
  public saldo14: string;
  public indice15: string;
  public saldo15: string;
}
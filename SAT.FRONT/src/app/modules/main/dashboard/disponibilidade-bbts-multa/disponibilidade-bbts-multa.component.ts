import { Component, Input, OnInit } from '@angular/core';
import { Filtro } from 'app/core/types/filtro.types';

@Component({
  selector: 'app-disponibilidade-bbts-multa',
  templateUrl: './disponibilidade-bbts-multa.component.html',
  styleUrls: ['./disponibilidade-bbts-multa.component.css']
})
export class DisponibilidadeBbtsMultaComponent implements OnInit {
  @Input() filtro: Filtro;
  public multaDisponibilidade: MultaDisponibilidadeModel[] = [];
  public loading: boolean = true; public totalDisp11: string;
  public totalDisp12: string;
  public totalDisp13: string;
  public totalDisp14: string;
  public totalDisp15: string;
  public totalDispTotal: string;

  constructor() { }

  ngOnInit(): void {
    this.obterDados();
  }
  private async obterDados() {

    for (let i = 0; i < 10; i++) {
      let d: MultaDisponibilidadeModel = new MultaDisponibilidadeModel();
      d.regiao = 'FRS';
      d.disp11 = 'R$100.00';
      d.disp12 = 'R$100.00';
      d.disp13 = 'R$100.00';
      d.disp14 = 'R$100.00';
      d.disp15 = 'R$100.00';
      d.total = 'R$100.00';
      this.multaDisponibilidade.push(d);
    }

    this.totalDisp11 = 'R$500.00';
    this.totalDisp12 = 'R$500.00';
    this.totalDisp13 = 'R$500.00';
    this.totalDisp14 = 'R$500.00';
    this.totalDisp15 = 'R$500.00';
    this.totalDispTotal = 'R$1500.00';

    this.loading = false;
  }

}

export class MultaDisponibilidadeModel {
  public regiao: string;
  public disp11: string;
  public disp12: string;
  public disp13: string;
  public disp14: string;
  public disp15: string;
  public total: string;
}

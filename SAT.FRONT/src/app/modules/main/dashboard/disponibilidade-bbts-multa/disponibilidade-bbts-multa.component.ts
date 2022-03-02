import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'app/core/services/dashboard.service';
import { DashboardViewEnum, ViewDashboardDisponibilidadeBBTSMultasDisponibilidade } from 'app/core/types/dashboard.types';
import Enumerable from 'linq';

@Component({
  selector: 'app-disponibilidade-bbts-multa',
  templateUrl: './disponibilidade-bbts-multa.component.html',
  styleUrls: ['./disponibilidade-bbts-multa.component.css']
})
export class DisponibilidadeBbtsMultaComponent implements OnInit {
  public viewDados: ViewDashboardDisponibilidadeBBTSMultasDisponibilidade[] = [];
  public multaDisponibilidade: MultaDisponibilidadeModel[] = [];
  public loading: boolean = true;
  public totalDisp11: number = 0;
  public totalDisp12: number = 0;
  public totalDisp13: number = 0;
  public totalDisp14: number = 0;
  public totalDisp15: number = 0;
  public totalDispTotal: number = 0;

  constructor(private _dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.obterDados();
  }

  private async obterDados() {

    this.viewDados = (await this._dashboardService.obterViewPorParametros({ dashboardViewEnum: DashboardViewEnum.BBTS_MULTA_DISPONIBILIDADE })
      .toPromise())
      .viewDashboardDisponibilidadeBBTSMultasDisponibilidade;

    for (let data of Enumerable.from(this.viewDados).orderBy(ord => ord.filial).groupBy(g => g.filial).toArray()) {
      let model: MultaDisponibilidadeModel = new MultaDisponibilidadeModel();
      model.regiao = data.key();
      model.disp11 = Enumerable.from(data).firstOrDefault(f => f.criticidade == '11')?.multa || 0;
      model.disp12 = Enumerable.from(data).firstOrDefault(f => f.criticidade == '12')?.multa || 0;
      model.disp13 = Enumerable.from(data).firstOrDefault(f => f.criticidade == '13')?.multa || 0;
      model.disp14 = Enumerable.from(data).firstOrDefault(f => f.criticidade == '14')?.multa || 0;
      model.disp15 = Enumerable.from(data).firstOrDefault(f => f.criticidade == '15')?.multa || 0;
      model.total = model.disp11 + model.disp12 + model.disp13 + model.disp14 + model.disp15;

      this.multaDisponibilidade.push(model);
    }

    this.totalDisp11 = Enumerable.from(this.multaDisponibilidade).sum(s => s.disp11);
    this.totalDisp12 = Enumerable.from(this.multaDisponibilidade).sum(s => s.disp12);
    this.totalDisp13 = Enumerable.from(this.multaDisponibilidade).sum(s => s.disp13);
    this.totalDisp14 = Enumerable.from(this.multaDisponibilidade).sum(s => s.disp14);
    this.totalDisp15 = Enumerable.from(this.multaDisponibilidade).sum(s => s.disp15);
    this.totalDispTotal = (this.totalDisp11 + this.totalDisp12 + this.totalDisp13 + this.totalDisp14 + this.totalDisp15);
    this.loading = false;
  }

}

export class MultaDisponibilidadeModel {
  public regiao: string;
  public disp11: number;
  public disp12: number;
  public disp13: number;
  public disp14: number;
  public disp15: number;
  public total: number;
}

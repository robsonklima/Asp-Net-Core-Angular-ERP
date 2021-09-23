import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { interval, Subject } from 'rxjs';
import { startWith, takeUntil } from 'rxjs/operators';
import { Dashboard, dashboardsConst } from 'app/core/types/dashboard.types'

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  
})
export class DashboardComponent implements AfterViewInit {
  @ViewChild('sidenav') sidenav: MatSidenav;
  dashboards: Dashboard[] = [];
  visaoSelecionada: number = 1;
  protected _onDestroy = new Subject<void>();
  filtro: any;

  constructor() {
    this.dashboards = dashboardsConst;
  }

  async ngAfterViewInit() {
    
    console.log(this.dashboards);

    this.dashboards.forEach((d) => {
      console.log(d.nome);
      
    })

    interval(.2 * 60 * 1000)
      .pipe(
          startWith(0),
          takeUntil(this._onDestroy)
      )
      .subscribe(() => {
          console.log('Atualizando');
          
      });
  }

  teste(e: string) {
    console.log(e);
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

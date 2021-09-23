import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { MatTabGroup } from '@angular/material/tabs';
import { DashboardEnum } from 'app/core/types/dashboard.types';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',

})
export class DashboardComponent implements AfterViewInit {
  @ViewChild("tabGroup", { static: false }) tabGroup: MatTabGroup;

  public get dashboardEnum(): typeof DashboardEnum {
    return DashboardEnum;
  }
  visaoSelecionada: string = this.dashboardEnum.PERFORMANCE_FILIAIS_RESULTADO_GERAL;
  slideSelecionado: number = 0;
  protected _onDestroy = new Subject<void>();

  constructor(
    private _cdr: ChangeDetectorRef
  ) { }

  async ngAfterViewInit() {
    interval(1 * 60 * 1000)
      .pipe(
        takeUntil(this._onDestroy)
      )
      .subscribe(() => {
        this.trocarDashboardOuSlide();
      });
  }

  private trocarDashboardOuSlide(): void {
    let dashboards: string[] = Object.values(this.dashboardEnum);

    if (this.slideSelecionado == this.tabGroup._tabs.length - 1) {
      let dashboardIndex = dashboards.indexOf(this.visaoSelecionada);

      if (dashboardIndex == dashboards.length - 1) {
        this.visaoSelecionada = dashboards[0];
      } else {
        this.visaoSelecionada = dashboards[dashboardIndex + 1];
      }

      this.slideSelecionado = 0;
    } else {
      this.slideSelecionado = this.tabGroup.selectedIndex + 1;
      this.tabGroup.selectedIndex = this.slideSelecionado;
    }

    this._cdr.detectChanges();
  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

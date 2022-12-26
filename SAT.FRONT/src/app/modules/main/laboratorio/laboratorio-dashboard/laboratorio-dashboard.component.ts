import { Component, OnInit } from '@angular/core';
import { interval, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-laboratorio-dashboard',
  templateUrl: './laboratorio-dashboard.component.html'
})
export class LaboratorioDashboardComponent implements OnInit {
  slidePausado: boolean = true;
  slideSelecionado: number = 0;
  slides: any = [
    'Recebidos e Reparados',
    'Pendências e Reparos',
    'Tempo Reparo',
    'Produtividade',
    'Antigos',
    'Reincidência',
    'Painel'
  ];
  protected _onDestroy = new Subject<void>();

  constructor() { }

  ngOnInit(): void {
    this.registrarEmitters();
  }

  registrarEmitters() {
    interval(5 * 1000 * 60)
      .pipe(takeUntil(this._onDestroy))
      .subscribe(() => {
        if (this.slidePausado)
          return;

        if (this.slides.length-1 == this.slideSelecionado)
          this.slideSelecionado = 0;
        else
          this.slideSelecionado++;
      });
  }

  ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}
}

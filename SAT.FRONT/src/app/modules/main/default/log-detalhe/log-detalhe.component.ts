import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { NLogService } from 'app/core/services/n-log.service';
import { NLogRegistro } from 'app/core/types/n-log.types';
import { Subject, fromEvent } from 'rxjs';
import { debounceTime, delay, distinctUntilChanged, map, takeUntil, tap } from 'rxjs/operators';

@Component({
  selector: 'app-log-detalhe',
  templateUrl: './log-detalhe.component.html'
})
export class LogDetalheComponent implements OnInit, AfterViewInit {
  logs: NLogRegistro[] = [];
  logsFiltrados: NLogRegistro[] = [];
  loading: boolean = true;
  opcoes: any = [{ nome: 'Todos', valor: 'ALL' }, { nome: 'Erro', valor: 'ERROR' }, { nome: 'Informação', valor: 'INFO' }];
  searchInputControl: FormControl = new FormControl();
  protected _onDestroy = new Subject<void>();

  constructor(
    private _nLogService: NLogService,
    private _snack: CustomSnackbarService
  ) { }

  ngAfterViewInit(): void {
    this.registrarEmitters();
  }

  ngOnInit(): void {
    this.obterLogs();
  }

  private async obterLogs(query: string = undefined) {
    this._nLogService.obterPorParametros({ filter: query }).subscribe((logs) => {
      this.logs = logs;
      this.logsFiltrados = logs;
      this.loading = false;
    }, e => {
      this._snack.exibirToast(`Ocorreu um erro ${e.message}`, 'error');
      this.loading = false;
    });
  }

  private registrarEmitters() {

  }

  ngOnDestroy() {
    this._onDestroy.next();
    this._onDestroy.complete();
  }
}

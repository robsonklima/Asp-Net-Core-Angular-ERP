import { Component, OnInit } from '@angular/core';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { NLogService } from 'app/core/services/n-log.service';
import { NLogRegistro } from 'app/core/types/n-log.types';

@Component({
  selector: 'app-log-detalhe',
  templateUrl: './log-detalhe.component.html'
})
export class LogDetalheComponent implements OnInit {
  logs: NLogRegistro[] = [];
  logsFiltrados: NLogRegistro[] = [];
  loading: boolean = true;
  opcoes:any = [ {nome: 'Todos', valor: 'ALL'}, { nome: 'Erro', valor: 'ERROR'}, { nome: 'Informação', valor: 'INFO' }];

  constructor(
    private _nLogService: NLogService,
    private _snack: CustomSnackbarService
  ) { }

  ngOnInit(): void {
    this._nLogService.obterPorParametros({}).subscribe((logs) => {
      this.logs = logs;
      this.logsFiltrados = logs;
      this.loading = false;
    }, e => {
      this._snack.exibirToast(`Ocorreu um erro ${e.message}`, 'error');
      this.loading = false;
    });
  }

  filtrarPorTipo(tipo: string) {
    if (tipo == 'ALL') {
      this.logsFiltrados = this.logs; 
    } else {
      this.logsFiltrados = this.logs.filter(log => log.level == tipo);
    }
  }
}

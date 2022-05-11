import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PontoHorariosListaComponent } from '../ponto-horarios-lista/ponto-horarios-lista.component';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { RelatorioAtendimentoService } from 'app/core/services/relatorio-atendimento.service';
import { RelatorioAtendimento, RelatorioAtendimentoData } from 'app/core/types/relatorio-atendimento.types';

@Component({
  selector: 'app-ponto-relatorios-atendimento',
  templateUrl: './ponto-relatorios-atendimento.component.html',
  styles: ['table { width: 100%; }']
})
export class PontoRelatoriosAtendimentoComponent implements OnInit {
  dataRegistro: string;
  codUsuario: string;
  dataSource: RelatorioAtendimento[] = [];
  displayedColumns: string[] = [
    'dataHoraInicio', 'dataHoraSolucao', 'codOS', 'numRAT', 'horarioInicioIntervalo', 'horarioTerminoIntervalo'
  ];
  loading: boolean;

  constructor(
    private _userService: UserService,
    private _relatorioAtendimentoService: RelatorioAtendimentoService,
    public dialogRef: MatDialogRef<PontoHorariosListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.dataRegistro = data.dataRegistro;
    this.codUsuario = data.codUsuario;
  }

  async ngOnInit() {
    this.loading = true;
    const usuario = await this._userService.obterPorCodigo(this.codUsuario).toPromise();

    const rats = await this._relatorioAtendimentoService.obterPorParametros({
      codTecnicos: usuario.codTecnico.toString(),
      dataInicio: moment(this.dataRegistro).format('yyyy-MM-DD 00:00:00'),
      dataSolucao: moment(this.dataRegistro).format('yyyy-MM-DD 23:59:59')
    }).toPromise();

    this.dataSource = rats.items;
    this.loading = false;
  }

  fechar(): void {
    this.dialogRef.close();
  }
}

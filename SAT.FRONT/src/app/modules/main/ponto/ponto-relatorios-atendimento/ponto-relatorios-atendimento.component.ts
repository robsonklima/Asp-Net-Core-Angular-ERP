import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { PontoHorariosListaComponent } from '../ponto-horarios-lista/ponto-horarios-lista.component';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

export interface RATData {
  codOS: number;
  numRAT: string;
  dataHoraInicio: string;
  dataHoraSolucao: string;
  local: string;
  cidade: string;
  uf: string;
  inicioIntervalo: string;
  fimIntervalo: string;
}

@Component({
  selector: 'app-ponto-relatorios-atendimento',
  templateUrl: './ponto-relatorios-atendimento.component.html',
  styles: ['table { width: 100%; }']
})
export class PontoRelatoriosAtendimentoComponent implements OnInit {
  dataRegistro: string;
  codUsuario: string;
  dataSource: RATData[];
  displayedColumns: string[] = [
    'dataHoraInicio', 'dataHoraSolucao', 'codOS', 'numRAT',
    'local', 'cidade', 'uf', 'inicioIntervalo', 'fimIntervalo'
  ];
  relatorios: RATData[] = [];

  constructor(
    private _userService: UserService,
    private _ordemServicoService: OrdemServicoService,
    public dialogRef: MatDialogRef<PontoHorariosListaComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.dataRegistro = data.dataRegistro;
    this.codUsuario = data.codUsuario;
  }

  async ngOnInit() {
    const usuario = await this._userService.obterPorCodigo(this.codUsuario).toPromise();

    const chamados = await this._ordemServicoService.obterPorParametros({
      codTecnico: usuario.codTecnico,
      dataHoraInicioInicio: moment(this.dataRegistro).format('yyyy-MM-DD 00:00:00'),
      dataHoraInicioFim: moment(this.dataRegistro).format('yyyy-MM-DD 23:59:59')
    }).toPromise();

    chamados.items.map(os => os.relatoriosAtendimento.map(r => {
      this.relatorios.push({
        codOS: r.codOS,
        numRAT: r.numRAT,
        local: os.localAtendimento.nomeLocal,
        uf: os.localAtendimento.cidade.unidadeFederativa.siglaUF,
        dataHoraInicio: r.dataHoraInicio,
        dataHoraSolucao: r.dataHoraSolucao,
        cidade: os.localAtendimento.cidade.nomeCidade,
        inicioIntervalo: r.horarioInicioIntervalo,
        fimIntervalo: r.horarioTerminoIntervalo
      });
    }));

    this.dataSource = this.relatorios;
  }

  fechar(): void {
    this.dialogRef.close();
  }
}

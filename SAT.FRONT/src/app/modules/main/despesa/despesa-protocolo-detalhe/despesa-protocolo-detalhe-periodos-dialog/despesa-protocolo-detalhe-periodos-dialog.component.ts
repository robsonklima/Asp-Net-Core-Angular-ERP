import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { DespesaProtocoloPeriodoTecnicoService } from 'app/core/services/despesa-protocolo-periodo-tecnico.service';
import { DespesaPeriodoTecnicoAtendimentoData } from 'app/core/types/despesa-adiantamento.types';
import { DespesaProtocoloPeriodoTecnico } from 'app/core/types/despesa-protocolo.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';

@Component({
  selector: 'app-despesa-protocolo-detalhe-periodos-dialog',
  templateUrl: './despesa-protocolo-detalhe-periodos-dialog.component.html'
})
export class DespesaProtocoloDetalhePeriodosDialogComponent implements OnInit
{
  userSession: UsuarioSessao;
  codDespesaProtocolo: number;
  aprovadas: DespesaPeriodoTecnicoAtendimentoData;
  isLoading: boolean = false;
  selectedOptions: number[] = [];

  constructor (
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<DespesaProtocoloDetalhePeriodosDialogComponent>,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
    private _despesaProtocoloTecnicoSvc: DespesaProtocoloPeriodoTecnicoService,
    private _userService: UserService) 
  {
    if (data)
      this.codDespesaProtocolo = data.codDespesaProtocolo;

    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit(): Promise<void> 
  {
    await this.loadData();
  }

  private async loadData()
  {
    this.isLoading = true;

    this.aprovadas = (await this._despesaPeriodoTecnicoSvc.obterPeriodosAprovados().toPromise());

    this.isLoading = false;
  }

  async confirmar()
  {
    this.selectedOptions.forEach(async option =>
    {
      var item: DespesaProtocoloPeriodoTecnico =
      {
        codDespesaProtocolo: this.codDespesaProtocolo,
        codDespesaPeriodoTecnico: option,
        codUsuarioCad: this.userSession.usuario.codUsuario,
        dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss'),
        indAtivo: 1
      };
      await this._despesaProtocoloTecnicoSvc.criar(item).toPromise();
    });

    this.dialogRef.close(true);
  }

  cancelar(): void
  {
    this.dialogRef.close(false);
  }
}

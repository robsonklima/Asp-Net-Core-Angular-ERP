import { Component, Inject, LOCALE_ID, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { fuseAnimations } from '@fuse/animations';
import { DespesaPeriodoTecnicoService } from 'app/core/services/despesa-periodo-tecnico.service';
import { TicketLogPedidoCreditoService } from 'app/core/services/ticket-log-pedido-credito.service';
import { DespesaCreditosCartaoListView, DespesaPeriodoTecnico } from 'app/core/types/despesa-periodo.types';
import { TecnicoCategoriaCreditoEnum } from 'app/core/types/tecnico.types';
import { TicketLogPedidoCredito } from 'app/core/types/ticketlog-types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import moment from 'moment';

@Component({
  selector: 'app-credito-creditar-dialog',
  templateUrl: './credito-creditar-dialog.component.html',
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations,
  providers: [{ provide: LOCALE_ID, useValue: "pt-BR" }]
})
export class CreditoCreditarDialogComponent {
  despesaCreditosCartaoListView: DespesaCreditosCartaoListView;
  despesaPeriodoTecnico: DespesaPeriodoTecnico;
  isLoading: boolean = false;
  userSession: UserSession;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private _despesaPeriodoTecnicoSvc: DespesaPeriodoTecnicoService,
    private _ticketLogPeridoCreditoSvc: TicketLogPedidoCreditoService,
    private _dialogRef: MatDialogRef<CreditoCreditarDialogComponent>,
    private _matDialog: MatDialog,
    private _userSvc: UserService) {
    if (data) {
      this.despesaCreditosCartaoListView = data.despesaCreditosCartaoListView;
      this.despesaPeriodoTecnico = data.despesaPeriodoTecnico;
    }

    this.userSession = JSON.parse(this._userSvc.userSession);
    this.obterDados();
  }

  async obterDados() {
    this.isLoading = true;
    this.isLoading = false;
  }

  getCategoriaCredito(c: TecnicoCategoriaCreditoEnum) {
    return TecnicoCategoriaCreditoEnum[c];
  }

  creditar(): void {
    const dialogRef = this._matDialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja CREDITAR o valor de ${this.despesaCreditosCartaoListView.combustivel
          .toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })} para o técnico ${this.despesaCreditosCartaoListView.tecnico}?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
      if (confirmacao) {
        this.despesaPeriodoTecnico.indCredito = 1;
        this.despesaPeriodoTecnico.codUsuarioCredito = this.userSession.usuario.codUsuario;
        this.despesaPeriodoTecnico.dataHoraCredito = moment().format('yyyy-MM-DD HH:mm:ss');

        this._despesaPeriodoTecnicoSvc.atualizar(this.despesaPeriodoTecnico).subscribe(() => {
          var pedidoCredito: TicketLogPedidoCredito =
          {
            codDespesaPeriodoTecnico: this.despesaPeriodoTecnico.codDespesaPeriodoTecnico,
            codUsuarioCad: this.userSession.usuario.codUsuario,
            dataHoraCad: moment().format('yyyy-MM-DD HH:mm:ss')
          }
  
          this._ticketLogPeridoCreditoSvc.criar(pedidoCredito).subscribe(() => {
  
          }, async () => {
            this.despesaPeriodoTecnico.indCredito = 0;
            this.despesaPeriodoTecnico.codUsuarioCredito = 'SAT';
  
            const despesa = await this._despesaPeriodoTecnicoSvc.atualizar(this.despesaPeriodoTecnico).toPromise();
          });
        });

        this._dialogRef.close(true);
      }
    });
  }

  compensar(): void {
    const dialogRef = this._matDialog.open(ConfirmacaoDialogComponent, {
      data: {
        titulo: 'Confirmação',
        message: `Deseja COMPENSAR o valor de ${this.despesaCreditosCartaoListView.combustivel.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })} para o técnico ${this.despesaCreditosCartaoListView.tecnico}?`,
        buttonText: {
          ok: 'Sim',
          cancel: 'Não'
        }
      }
    });

    dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
      if (confirmacao) {
        this.despesaPeriodoTecnico.indCompensacao = 1;
        this.despesaPeriodoTecnico.codUsuarioCompensacao = this.userSession.usuario.codUsuario;
        this.despesaPeriodoTecnico.dataHoraCompensacao = moment().format('yyyy-MM-DD HH:mm:ss');

        this._despesaPeriodoTecnicoSvc.atualizar(this.despesaPeriodoTecnico).toPromise();
        this._dialogRef.close(true);
      }
    });
  }

  cancelar(): void {
    this._dialogRef.close(false);
  }
}
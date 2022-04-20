import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import moment from 'moment';
import { OrdemServicoDetalheComponent } from '../ordem-servico-detalhe/ordem-servico-detalhe.component';

@Component({
  selector: 'app-ordem-servico-cancelamento',
  templateUrl: './ordem-servico-cancelamento.component.html'
})
export class OrdemServicoCancelamentoComponent {
  os: OrdemServico;
  userSession: UserSession;

  constructor(
    private _userSvc: UserService,
    private _osSvc: OrdemServicoService,
    private _snack: CustomSnackbarService,
    public dialogRef: MatDialogRef<OrdemServicoDetalheComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.os = data.os;
    this.userSession = JSON.parse(this._userSvc.userSession);
  }

  cancelar() {
    this.dialogRef.close();
  }

  salvar() {
    this.os.codStatusServico = StatusServicoEnum.CANCELADO;
    this.os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm');
    this.os.codUsuarioManut = this.userSession.usuario.codUsuario;
    this.os.dataHoraCancelamento = moment().format('YYYY-MM-DD HH:mm');
    this.os.codUsuarioCancelamento = this.userSession.usuario.codUsuario;
    
    this._osSvc.atualizar(this.os).subscribe((os: OrdemServico) => {
      this._snack.exibirToast("Chamado cancelado com sucesso!", "success");
      this.dialogRef.close({ os: this.os });
    });
  }
}

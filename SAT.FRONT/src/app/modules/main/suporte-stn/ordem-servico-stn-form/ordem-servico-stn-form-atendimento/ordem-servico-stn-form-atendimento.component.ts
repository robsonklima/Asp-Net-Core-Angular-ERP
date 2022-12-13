import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { OrdemServicoSTNService } from 'app/core/services/ordem-servico-stn.service';
import { StatusServicoSTNService } from 'app/core/services/status-servico-stn.service';
import { OrdemServicoSTN } from 'app/core/types/ordem-servico-stn.types';
import { StatusServicoSTN } from 'app/core/types/status-servico-stn.types';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';

@Component({
  selector: 'app-ordem-servico-stn-form-atendimento',
  templateUrl: './ordem-servico-stn-form-atendimento.component.html'
})
export class OrdemServicoStnFormAtendimentoComponent implements OnInit {
    @Input() codAtendimento: number;
    atendimento: OrdemServicoSTN;
    status: StatusServicoSTN;
    userSession: UsuarioSessao;

  constructor(
    private _ordemServicoSTNService: OrdemServicoSTNService,
    private _statusSTNService: StatusServicoSTNService,
    private _cdr: ChangeDetectorRef,
    private _userService: UserService,
    private _dialog: MatDialog,
    private _router: Router
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit(){
    this.atendimento = await this._ordemServicoSTNService.obterPorCodigo(this.codAtendimento).toPromise();
    this.status = await this._statusSTNService.obterPorCodigo(this.atendimento.codStatusSTN).toPromise();
  }

  onChange(){}
}

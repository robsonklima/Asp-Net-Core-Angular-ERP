import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AgendamentoService } from 'app/core/services/agendamento.service';
import { Agendamento, AgendamentoData } from 'app/core/types/agendamento.types';
import { UserService } from 'app/core/user/user.service';
import { UserSession } from 'app/core/user/user.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';

@Component({
  selector: 'app-ordem-servico-agendamentos',
  templateUrl: './ordem-servico-agendamentos.component.html'
})
export class OrdemServicoAgendamentosComponent implements OnInit {
  @Input() codOS: number;
  agendamentos: Agendamento[] = [];
  isLoading: boolean = true;
  userSession: UserSession;

  constructor(
    private _agendamentoService: AgendamentoService,
    private _userService: UserService,
    private _dialog: MatDialog
  ) {
    this.userSession = JSON.parse(this._userService.userSession);
  }

  async ngOnInit() {
    this.agendamentos = (await this.obterAgendamentos()).items;
    this.isLoading = false;
  }

  private async obterAgendamentos(): Promise<AgendamentoData> {
    return this._agendamentoService
      .obterPorParametros({ codOS: this.codOS })
      .toPromise();
  }

  public removerAgendamento(agendamento: Agendamento) {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja remover este agendamento?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});
	
		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao) {
				this._agendamentoService.deletar(agendamento.codAgendamento).subscribe(() => {
					this.obterAgendamentos();
				});
			}
		});
	}
}

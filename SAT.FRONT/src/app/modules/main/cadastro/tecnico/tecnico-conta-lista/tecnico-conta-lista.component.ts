import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { TecnicoContaService } from 'app/core/services/tecnico-conta-service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Tecnico, TecnicoConta } from 'app/core/types/tecnico.types';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { TecnicoContaFormDialogComponent } from '../tecnico-conta-form-dialog/tecnico-conta-form-dialog.component';

@Component({
  selector: 'app-tecnico-conta-lista',
  templateUrl: './tecnico-conta-lista.component.html'
})
export class TecnicoContaListaComponent implements OnInit {
  @Input() codTecnico;
  tecnico: Tecnico;
  contas: TecnicoConta[] = [];
  loading: boolean;

  constructor(
    private _tecnicoService: TecnicoService,
    private _tecnicoContaService: TecnicoContaService,
    private _snack: CustomSnackbarService,
    private _dialog: MatDialog
  ) { }

  async ngOnInit() {
    if (!this.codTecnico) return;

    this.tecnico = await this._tecnicoService.obterPorCodigo(this.codTecnico).toPromise();

    this.obterContasBancarias();
  }

  private async obterContasBancarias() {
    const data = await this._tecnicoContaService.obterPorParametros({ codTecnico: this.codTecnico }).toPromise();

    this.contas = data.items;
  }

  public onRemover(conta: TecnicoConta) {
    const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja excluir esta conta?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao) {
				this._tecnicoContaService.deletar(conta.codTecnicoConta).subscribe(() => {
          this._snack.exibirToast('Conta removida com sucesso', 'success');
        }, () => {
          this._snack.exibirToast('Erro ao remover a conta', 'error');
        });
			}
		});
  }

  public onEditar(conta: TecnicoConta) { 
    this._dialog.open(TecnicoContaFormDialogComponent, {
			data: {
        conta: conta
      }
		});
  }
}

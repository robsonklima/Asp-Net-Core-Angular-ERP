import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Auditoria } from 'app/core/types/auditoria.types';
import { MatDialog } from '@angular/material/dialog';
import { AuditoriaService } from 'app/core/services/auditoria.service';
import { first } from 'rxjs/operators';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuditoriaUtilizacaoDialogComponent } from '../auditoria-utilizacao/auditoria-utilizacao-dialog/auditoria-utilizacao-dialog.component';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { UserSession } from 'app/core/user/user.types';
import { UserService } from 'app/core/user/user.service';
import moment from 'moment';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { AuditoriaVeiculoService } from 'app/core/services/auditoria-veiculo.service';
import { AuditoriaVeiculo } from 'app/core/types/auditoria-veiculo.types';
import { SetorEnum } from 'app/core/types/setor.types';

@Component({
	selector: 'app-auditoria-layout',
	templateUrl: './auditoria-layout.component.html',
})
export class AuditoriaLayoutComponent implements OnInit {

	public codAuditoria: number;
	public nroAuditoria: string;
	public Auditoria: Auditoria;
	auditoria: Auditoria;
	userSession: UserSession;
	form: FormGroup;

	constructor(
		private _route: ActivatedRoute,
		private _router: Router,
		private _dialog: MatDialog,
		private _snack: CustomSnackbarService,
		private _auditoriaService: AuditoriaService,
		private _auditoriaVeiculoSrv: AuditoriaVeiculoService,
		private _userSvc: UserService,
		private _formBuilder: FormBuilder,
	) {
		this.userSession = JSON.parse(this._userSvc.userSession);
	}

	async ngOnInit(): Promise<void> {
		this.codAuditoria = +this._route.snapshot.paramMap.get('codAuditoria');
		this.obterAuditoria();
		this.inicializarForm();
	}

	private inicializarForm() {
		this.form = this._formBuilder.group({
			codAuditoria: [
				{
					value: undefined,
					disabled: true
				}
			],
		});
	}

	reciverFeedback(codFilho) {
		this.nroAuditoria = codFilho;
	}

	obterAuditoria() {
		this._auditoriaService.obterPorCodigo(this.codAuditoria)
			.pipe(first())
			.subscribe(data => {
				this.form.patchValue(data);
				this.auditoria = data;
			});
	}

	editarAuditoria() {
		const dialogRef = this._dialog.open(AuditoriaUtilizacaoDialogComponent, {
			data: {
				codAuditoria: this.codAuditoria,
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao)
				this.obterAuditoria();
		});
	}

	finalizar() {
		const form: any = this.form.getRawValue();

		let obj = {
			...this.auditoria,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario,
				codAuditoriaStatus: 3,
			}
		};

		this._auditoriaService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Auditoria finalizada com sucesso!", "success");
		}, e => {
			this.form.enable();
		});
	}

	reabrir() {
		const form: any = this.form.getRawValue();

		let obj = {
			...this.auditoria,
			...form,
			...{
				dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
				codUsuarioManut: this.userSession.usuario.codUsuario,
				codAuditoriaStatus: 2,
			}
		};

		this._auditoriaService.atualizar(obj).subscribe(() => {
			this._snack.exibirToast("Auditoria reaberta com sucesso!", "success");
		}, e => {
			this.form.enable();
		});
	}

	excluir() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data:
			{
				titulo: 'Confirmação',
				mensagem: 'Deseja confirmar a exclusão desta auditoria?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				this._auditoriaService.deletar(this.codAuditoria).subscribe(() => {
					this._snack.exibirToast('Auditoria Excluída', 'sucess');
					this._router.navigate(['/frota-tecnico/']);
				})

			}
		});
	}

	async inserirNivelCombustivel() {
		const audVeiculo = await this._auditoriaVeiculoSrv.obterPorCodigo(this.auditoria.codAuditoriaVeiculo).toPromise();
		audVeiculo.codAuditoriaVeiculoTanque = 1;


		this._auditoriaVeiculoSrv.atualizar(audVeiculo).subscribe(() => {
			this._snack.exibirToast('Tanque atualizado!', 'sucess');
		});

	}

	validarPermissao() {
		if (this.userSession.usuario.codSetor == +SetorEnum.GERENCIA || this.userSession.usuario.codPerfil == +PerfilEnum.ADM_DO_SISTEMA) {
			return true;
		}
		else {
			return false;
		}
	}

}

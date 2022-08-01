import { AfterViewInit, ChangeDetectorRef, Component, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { Foto } from 'app/core/types/foto.types';
import { OrdemServico, StatusServicoEnum } from 'app/core/types/ordem-servico.types';
import { MatDialog } from '@angular/material/dialog';
import { OrdemServicoAgendamentoComponent } from '../ordem-servico-agendamento/ordem-servico-agendamento.component';
import { CustomSnackbarService } from 'app/core/services/custom-snackbar.service';
import { AgendamentoService } from 'app/core/services/agendamento.service';
import { MatSidenav } from '@angular/material/sidenav';
import { StatusServico, statusServicoConst } from 'app/core/types/status-servico.types';
import moment from 'moment';
import { UsuarioSessao } from 'app/core/types/usuario.types';
import { UserService } from 'app/core/user/user.service';
import { ConfirmacaoDialogComponent } from 'app/shared/confirmacao-dialog/confirmacao-dialog.component';
import Enumerable from 'linq';
import { RoleEnum } from 'app/core/user/user.types';
import { AgendaTecnicoService } from 'app/core/services/agenda-tecnico.service';
import { OrdemServicoCancelamentoComponent } from '../ordem-servico-cancelamento/ordem-servico-cancelamento.component';
import { OrdemServicoEmailDialogComponent } from '../ordem-servico-email-dialog/ordem-servico-email-dialog.component';
import { FotoService } from 'app/core/services/foto.service';
import { OrdemServicoHistorico, OrdemServicoHistoricoData } from 'app/core/types/ordem-servico-historico.types';
import { OrdemServicoHistoricoService } from 'app/core/services/ordem-servico-historico.service';
import { fuseAnimations } from '@fuse/animations';
import { TipoIntervencaoEnum } from 'app/core/types/tipo-intervencao.types';
import { PerfilEnum } from 'app/core/types/perfil.types';
import { AgendaTecnico, AgendaTecnicoTipoEnum } from 'app/core/types/agenda-tecnico.types';
import { DispBBBloqueioOS, DispBBBloqueioOSParameters } from 'app/core/types/DispBBBloqueioOS.types';
import { DispBBBloqueioOSService } from 'app/core/services/disp-bb-bloqueio-os.service';
import { IntegracaoCobraService } from 'app/core/services/integracao-cobra.service';
import { CheckinCheckoutService } from 'app/core/services/checkin-checout.service';
import { CheckinCheckout } from 'app/core/types/checkin-checkout.types';

@Component({
	selector: 'app-ordem-servico-detalhe',
	templateUrl: './ordem-servico-detalhe.component.html',
	styles: [`
		.tool-tip{
			white-space: pre-line;
		}
	`],
	animations: fuseAnimations,
	encapsulation: ViewEncapsulation.None,
})
export class OrdemServicoDetalheComponent implements AfterViewInit {
	@ViewChild('sidenav') sidenav: MatSidenav;
	codOS: number;
	os: OrdemServico;
	statusServico: StatusServico;
	perfis: any;
	userSession: UsuarioSessao;
	qtdFotos: number = 0;
	qtdLaudos: number = 0;
	ultimoAgendamento: string;
	histAgendamento: string = 'Agendamentos: \n';
	isLoading: boolean = false;
	historico: any[] = [];
	dispBBBloqueioOS: DispBBBloqueioOS[] = [];
	checkinsCheckouts: CheckinCheckout[] = [];
	validaCliente:boolean = this._userService.isCustomer;

	public get tipoIntervencaoEnum(): typeof TipoIntervencaoEnum {
		return TipoIntervencaoEnum;
	}

	public get perfilEnum(): typeof RoleEnum {
		return RoleEnum;
	}

	constructor(
		private _route: ActivatedRoute,
		private _ordemServicoService: OrdemServicoService,
		private _osHistoricoService: OrdemServicoHistoricoService,
		private _agendamentoService: AgendamentoService,
		private _userService: UserService,
		private _snack: CustomSnackbarService,
		private _cdr: ChangeDetectorRef,
		private _dialog: MatDialog,
		private _agendaTecnicoService: AgendaTecnicoService,
		private _ordemServicoHistoricoSvc: OrdemServicoHistoricoService,
		private _fotoService: FotoService,
		private _dispBBBloqueioOSService: DispBBBloqueioOSService,
		private _integracaoCobraService: IntegracaoCobraService,
		private _checkinCheckoutService: CheckinCheckoutService
	) {
		this.userSession = JSON.parse(this._userService.userSession);
	}

	ngAfterViewInit(): void {
		this.codOS = +this._route.snapshot.paramMap.get('codOS');

		if (this.codOS) {
			this.obterDados();
			this.perfis = RoleEnum;
			this.sidenav.closedStart.subscribe(() => this.obterDados());
		}

		this._cdr.detectChanges();
	}

	private async obterDados() {
		this.isLoading = true; 0
		await this.obterOS();
		this.obterHistorico();
		this.obterCheckinsECheckouts();
		this.obterAgendamentos();
		this.obterFotosRAT();
		this.obterQtdLaudos();
		this.obterDispBBBloqueioOS();
		this.isLoading = false;
	}

	private async obterOS() {
		this.os = await this._ordemServicoService.obterPorCodigo(this.codOS).toPromise();
	}

	private async obterHistorico() {
		const historico = await this._osHistoricoService.obterPorParametros({
			codOS: this.codOS,
			sortDirection: 'asc',
			sortActive: 'dataHoraCad'
		}).toPromise();

		this.historico.push.apply(this.historico, historico.items);
	}

	private async obterCheckinsECheckouts() {
		const cks = await this._checkinCheckoutService.obterPorParametros({
			codOS: this.codOS,
			sortDirection: 'asc',
			sortActive: 'dataHoraCad'
		}).toPromise();

		this.historico.concat();
		this.historico.push.apply(this.historico, cks.items);
		this.historico = this.historico
			.sort((a, b) => (moment(a.dataHoraCadSmartphone || a.dataHoraCad) > moment(b.dataHoraCadSmartphone || b.dataHoraCad)) ? 1 : -1);
	}

	private obterHistoricoOS(codOS: number): Promise<OrdemServicoHistoricoData> {
		return new Promise((resolve, reject) => {
			this._ordemServicoHistoricoSvc
				.obterPorParametros({ codOS: codOS })
				.subscribe((historico: OrdemServicoHistoricoData) => {
					resolve(historico);
				}, () => {
					reject();
				});
		})
	}

	private async obterAgendamentos() {
		if (this.os.agendamentos?.length) {
			var agendamentos = Enumerable.from(this.os.agendamentos)
				.orderByDescending(a => a.codAgendamento);

			this.histAgendamento += agendamentos
				.select(e => moment(e.dataAgendamento)
					.format('DD/MM HH:mm'))
				.toJoinedString(", \n");

			this.ultimoAgendamento = agendamentos
				.select(a => a.dataAgendamento)
				.first();
		}
	}

	async agendar() {
		const dialogRef = this._dialog.open(OrdemServicoAgendamentoComponent, {
			data: {
				codOS: this.os.codOS
			}
		});

		dialogRef.afterClosed().subscribe(async (data: any) => {
			if (data) {

				if (data.agendamento.dataAgendamento < moment().format('YYYY-MM-DD HH:mm:ss')) {

					this._snack.exibirToast('O Chamado não deve ser agendado em datas retroativas', 'error');
					return;
				}

				this._agendamentoService.criar(data.agendamento).subscribe(async () => {
					this.os.dataHoraSolicitacao = data.agendamento.dataAgendamento;

					this._ordemServicoService.atualizar(this.os).subscribe(async () => {
						this._snack.exibirToast('Chamado agendado com sucesso!', 'success');
						await this.criarAgendaTecnico();
						this.obterDados();
					},
						error => {
							this._snack.exibirToast('Erro ao agendar chamado.', 'error');
						});
				},
					() => {
						this._snack.exibirToast('Erro ao agendar chamado.', 'error');
					});
			}
		});
	}

	openEmailDialog() {
		const parametros = {
			width: '600px',
			data: { os: this.os }
		};

		this._dialog.open(OrdemServicoEmailDialogComponent, parametros);
	}

	cancelar() {
		const dialogRef = this._dialog.open(OrdemServicoCancelamentoComponent, {
			width: '400px',
			data: { os: this.os }
		});

		dialogRef.afterClosed().subscribe((os: any) => {
			if (os) {
				this.obterDados();
			}
		});
	}

	public verificarPermissaoCancelamento(): boolean {
		if (this.userSession.usuario.codPerfil === PerfilEnum.ADM_DO_SISTEMA && this.os?.codStatusServico !== StatusServicoEnum.FECHADO && this.os?.codStatusServico !== StatusServicoEnum.CANCELADO)
			return false;

		if (this.userSession.usuario.codPerfil === PerfilEnum.PV_COORDENADOR_DE_CONTRATO && this.os?.codStatusServico !== StatusServicoEnum.FECHADO && this.os?.codStatusServico !== StatusServicoEnum.CANCELADO)
			return false;

		if (
				(
					this.userSession.usuario.codPerfil === PerfilEnum.FILIAL_LIDER_C_FUNCOES_COORDENADOR ||
					this.userSession.usuario.codPerfil === PerfilEnum.FILIAL_LIDER_DE_SETOR ||
					this.userSession.usuario.codPerfil === PerfilEnum.FILIAL_COORDENADOR ||
					this.userSession.usuario.codPerfil === PerfilEnum.FILIAIS_SUPERVISOR
				) && 
				this.os?.codTipoIntervencao === TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO && 
				this.os?.codStatusServico !== StatusServicoEnum.FECHADO && 
				this.os?.codStatusServico !== StatusServicoEnum.CANCELADO
			)
			return false;

		return true;
	}

	public verificarPermissaoReabertura(): boolean {
		if (this.os?.codStatusServico !== StatusServicoEnum.FECHADO)
			return false;

		if (this.userSession.usuario.perfil?.codPerfil === this.perfilEnum.PV_COORDENADOR_DE_CONTRATO)
			return true;

		if (this.userSession.usuario.perfil?.codPerfil === this.perfilEnum.ADMIN)
			return true;

		if (this.os?.codTipoIntervencao === this.tipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO)
			return true;

		return false;
	}

	async cancelarTransferencia() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja cancelar a transferência deste chamado?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe(async (confirmacao: boolean) => {
			if (confirmacao) {
				var ultimoStatus = statusServicoConst.ABERTO;

				this.obterHistoricoOS(this.codOS).then(async (historico) => {
					const historicoOS = historico.items.filter(h => h.codStatusServico != StatusServicoEnum.TRANSFERIDO);

					if (historicoOS.length)
						ultimoStatus = Enumerable.from(historicoOS)
							.orderByDescending(i => i.codHistOS)
							.where(i => i.codStatusServico !== StatusServicoEnum.FECHADO)
							.firstOrDefault()
							.codStatusServico;

					let obj = {
						...this.os,
						...{
							dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
							codUsuarioManut: this.userSession.usuario.codUsuario,
							codStatusServico: ultimoStatus,
							codTecnico: null
						}
					};

					Object.keys(obj).forEach((key) => typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key]);
					this._ordemServicoService.atualizar(obj).subscribe(
						async () => {
							this._snack.exibirToast("Transferência cancelada com sucesso!", "success");
							this.obterDados();
						},
						() => {
							this._snack.exibirToast("Erro ao cancelar transferência!", "error");
						});
				});
			}
		});
	}

	async reabrir() {
		const dialogRef = this._dialog.open(ConfirmacaoDialogComponent, {
			data: {
				titulo: 'Confirmação',
				message: 'Deseja reabrir este chamado?',
				buttonText: {
					ok: 'Sim',
					cancel: 'Não'
				}
			}
		});

		dialogRef.afterClosed().subscribe((confirmacao: boolean) => {
			if (confirmacao) {
				let obj: OrdemServico = {
					...this.os,
					...{
						codStatusServico: statusServicoConst.ABERTO,
						dataHoraManut: moment().format('YYYY-MM-DD HH:mm:ss'),
						codUsuarioManut: this.userSession.usuario?.codUsuario
					}
				};

				Object.keys(obj).forEach((key) => {
					typeof obj[key] == "boolean" ? obj[key] = +obj[key] : obj[key] = obj[key];
				});

				this._ordemServicoService.atualizar(obj).subscribe((os) => {
					this._snack.exibirToast("Ordem de serviço reaberta!", "success");
					this.obterOS();
				});
			}
		});
	}

	private async obterFotosRAT() {
		for (const [i, rat] of this.os.relatoriosAtendimento.entries()) {
			this.os.relatoriosAtendimento[i].fotos =
				(await this._fotoService.obterPorParametros(
					{
						codOS: rat.codOS,
						numRAT: rat.numRAT,
						sortActive: 'CodRATFotoSmartphone',
						sortDirection: 'desc'
					}
				).toPromise()).items.filter(f => !f.modalidade.includes('ASSINATURA'));

			this.qtdFotos = this.qtdFotos + this.os.relatoriosAtendimento[i].fotos.length;
		}
	}

	private obterQtdLaudos() {
		this.os.relatoriosAtendimento.forEach((rat) => {
			this.qtdLaudos += rat.laudos.length;
		});
	}

	filtrarFotosRAT(tipo: string, fotos: Foto[]): Foto[] {
		let fotosFiltered: Foto[];

		if (tipo === 'RAT') {
			fotosFiltered = fotos.filter(f => !f.modalidade.includes('LAUDO'));
		} else if (tipo === 'LAUDO') {
			fotosFiltered = fotos.filter(f => f.modalidade.includes('LAUDO'));
		}

		return fotosFiltered;
	}

	private criarAgendaTecnico() {
		if (this.os.codTecnico == null) return;

		const agenda: AgendaTecnico = {
			codTecnico: this.os.codTecnico,
			codOS: this.os.codOS,
			codUsuarioCad: this.userSession.usuario.codUsuario,
			dataHoraCad: moment().format('YYYY-MM-DD HH:mm:ss'),
			indAtivo: 1,
			inicio: null,
			fim: null,
			indAgendamento: 0,
			tipo: AgendaTecnicoTipoEnum.OS
		}

		this._agendaTecnicoService.criar(agenda).subscribe(() => { });
	}

	public async bloquearDesbloquearChamado() {
		this.isLoading = true;

		this.os.indBloqueioReincidencia = this.os.indBloqueioReincidencia ? 0 : 1;
		this.os.codUsuarioManut = this.userSession.usuario.codUsuario,
			this.os.dataHoraManut = moment().format('YYYY-MM-DD HH:mm:ss'),

			await this._ordemServicoService.atualizar(this.os).toPromise();
		await this.obterDados();

		this.isLoading = false;
	}

	private async obterDispBBBloqueioOS() {
		let params: DispBBBloqueioOSParameters = {
			indAtivo: 1,
			codOS: this.os.codOS
		}

		const data = await this._dispBBBloqueioOSService
			.obterPorParametros(params)
			.toPromise();

		this.dispBBBloqueioOS = data.items;
	}

	public async desbloqueioRATBBTS() {
		this.isLoading = true;

		const osBBBloqueio = (await this._dispBBBloqueioOSService.obterPorParametros({ codOS: this.codOS }).toPromise()).items.shift();
		osBBBloqueio.indAtivo = 0;

		this._dispBBBloqueioOSService.atualizar(osBBBloqueio).subscribe(r => {
			this._snack.exibirToast('Desbloqueio Realizado!', 'success');
		});

		await this.obterDados();

		this.isLoading = false;
	}

	public async reenvioIntegracaoBBTS() {
		this.isLoading = true;

		this._integracaoCobraService.deletar(this.codOS).subscribe((r) => {
			this._snack.exibirToast('Reenvio realizado!', 'success');
		}, (error) => {
			this._snack.exibirToast('Erro ao reenviar!', 'error');
		}
		);

		await this.obterDados();
		this.isLoading = false;
	}

	verificaPermissaoBB() {
		if (((this.userSession.usuario.codPerfil == PerfilEnum.PV_COORDENADOR_DE_CONTRATO ||
			this.userSession.usuario.codPerfil == PerfilEnum.ADM_DO_SISTEMA) &&
			this.os?.codCliente == 1)) {
			return true;
		}
		return false
	}

	isOrcamento() {
		var orcamentos = [
			TipoIntervencaoEnum.ORCAMENTO,
			TipoIntervencaoEnum.ORC_APROVADO,
			TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE,
			TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO,
			TipoIntervencaoEnum.ORC_REPROVADO]

		return orcamentos.includes(this.os?.codTipoIntervencao);
	}

	getTimeFromMins(mins) {
		var h = mins / 60 | 0,
			m = mins % 60 | 0;
		return moment.utc().hours(h).minutes(m).format("HH:mm");
	}

	trocarTab(tab: any) {

	}
}